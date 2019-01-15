﻿
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();


            foreach (CompanyProfilePoco item in pocos)
            {
                if(!(string.IsNullOrEmpty(item.CompanyWebsite)))
                {
                    string[] requiredURLExtention = new string[] { "ca", "com", "biz" };

                    string extention = item.CompanyWebsite.TrimEnd('.');
                    if (!requiredURLExtention.Any(t => item.CompanyWebsite.Contains(t)))
                    {
                        exceptions.Add(new ValidationException(600, $"Website is not Valid,Valid website extentions are '.ca', '.com', '.biz' !"));

                    }
                }
                if(string.IsNullOrEmpty(item.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, $"PhoneNumber is not in the required format."));
                }

                if (!string.IsNullOrEmpty(item.ContactPhone))                
                {
                    string[] phoneComponents = item.ContactPhone.Split('-');

                    if (phoneComponents.Length < 3)
                    {
                        exceptions.Add(new ValidationException(601, $"PhoneNumber is not in the required format."));
                    }
                    else
                    {
                        if (phoneComponents[0].Length < 3)
                        {
                            exceptions.Add(new ValidationException(601, $"PhoneNumber is not in the required format."));
                        }
                        else if (phoneComponents[1].Length < 3)
                        {
                            exceptions.Add(new ValidationException(601, $"PhoneNumber is not in the required format."));
                        }
                        else if (phoneComponents[2].Length < 4)
                        {
                            exceptions.Add(new ValidationException(601, $"PhoneNumber is not in the required format."));
                        }
                    }
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
