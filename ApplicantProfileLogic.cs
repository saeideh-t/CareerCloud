
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository)
        {
        }
        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantProfilePoco item in pocos)
            {
                if (item.CurrentSalary < 0)
                {
                    exceptions.Add(new ValidationException(111, $"CurrentSalary cannot be Negative"));
                }
                if (item.CurrentRate < 0)
                {
                    exceptions.Add(new ValidationException(112, $"CurrentRate cannt be Negative"));
                }
                
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
