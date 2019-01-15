
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {

        }
        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantEducationPoco item in pocos)
            {
                if (!string.IsNullOrEmpty(item.Major))
                {
                    if (item.Major.Length < 3)
                    {
                        exceptions.Add(new ValidationException(107, $"Major is too short."));
                    }
                }
                if (item.StartDate > DateTime.Now  )
                {
                    exceptions.Add(new ValidationException(108, $"Start Date is Invalid."));
                }
                if (item.CompletionDate < item.StartDate )
                {
                    exceptions.Add(new ValidationException(109, $"Completion Date is Invalid."));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}

