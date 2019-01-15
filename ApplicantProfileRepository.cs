﻿
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : BaseADO, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantProfilePoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [Applicant_Profiles] 
                                      ([Id],[Login],[Current_Salary],[Current_Rate],[Currency],[Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code])  
                                     VALUES (@Id,@Login,@Current_Salary,@Current_Rate,@Currency,@Country_Code,@State_Province_Code,@Street_Address,@City_Town,@Zip_Postal_Code)";
                     
                    
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Login", poco.Login);
                    command.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    command.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    command.Parameters.AddWithValue("@Currency", poco.Currency);
                    command.Parameters.AddWithValue("@Country_Code", poco.Country);
                    command.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    command.Parameters.AddWithValue("@Street_Address", poco.Street);
                    command.Parameters.AddWithValue("@City_Town", poco.City);
                    command.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);
                    

                    conn.Open();
                    int rowEffected = command.ExecuteNonQuery();
                    conn.Close();
                
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[1500];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Applicant_Profiles", conn);
                conn.Open();
                int position = 0;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    if (!reader.IsDBNull(2))
                    {
                        poco.CurrentSalary = reader.GetDecimal(2);
                    }
                    else
                    {
                        poco.CurrentSalary = null;
                    }
                    if (!reader.IsDBNull(3))
                    {
                        poco.CurrentRate = reader.GetDecimal(3);
                    }
                    else
                    {
                        poco.CurrentRate = null;
                    }

                    poco.Currency = reader.GetString(4);
                    poco.Country = reader.GetString(5);
                    poco.Province = reader.GetString(6);
                    poco.Street = reader.GetString(7);
                    poco.City = reader.GetString(8);
                    poco.PostalCode = reader.GetString(9);
                    poco.TimeStamp = (byte[])reader[10];
                    pocos[position] = poco;
                    position++;


                }

                conn.Close();
                return pocos.Where(a => a != null).ToList();
            }

            
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos =
                           GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantProfilePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [Applicant_Profiles] Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int numOfRows = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantProfilePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Applicant_Profiles 
                        SET Login = @Login, 
                            Current_Salary=@Current_Salary, 
                            Current_Rate = @Current_Rate,
                            Currency = @Currency,
                            Country_Code = @Country_Code,
                            State_Province_Code = @State_Province_Code,
                            Street_Address=@Street_Address,
                            City_Town=@City_Town,
                            Zip_Postal_Code=@Zip_Postal_Code 
                            
                        WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.Country);                    
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);
                    

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int NumOfRow = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
