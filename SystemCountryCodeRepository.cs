﻿using CareerCloud.DataAccessLayer;
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
    public class SystemCountryCodeRepository : BaseADO, IDataRepository<SystemCountryCodePoco>
    {
        public void Add(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemCountryCodePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [System_Country_Codes]
                        ([Code],[Name])  
                        VALUES
                        (@Code, @Name)";
                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    cmd.Parameters.AddWithValue("Name", poco.Name);                    
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT [Code],[Name] 
                                        FROM [JOB_PORTAL_DB].[dbo].[System_Country_Codes]";
                conn.Open();
                int x = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                SystemCountryCodePoco[] appPocos = new SystemCountryCodePoco[1300];
                while (reader.Read())
                {
                    SystemCountryCodePoco poco = new SystemCountryCodePoco();
                    poco.Code = reader.GetString(0);
                    poco.Name = reader.GetString(1);                    
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemCountryCodePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [System_Country_codes] Where Code= @Code";
                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    conn.Open();
                    int numOfRows = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemCountryCodePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE System_Country_Codes 
                        SET Name = @Name  
                        WHERE Code = @Code";
                    cmd.Parameters.AddWithValue("@Name", poco.Name);                    
                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    conn.Open();
                    int NumOfRow = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
