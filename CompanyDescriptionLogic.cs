
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>

    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
        {
        }

        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyDescriptionPoco item in pocos)
            {
                if (!string.IsNullOrEmpty(item.CompanyDescription))
                {
                    if (item.CompanyDescription.Length < 3)
                    {
                        exceptions.Add(new ValidationException(107, $"Company Description must be greater than two characters"));
                    }
                }
                if (!string.IsNullOrEmpty(item.CompanyName))
                {
                    if (item.CompanyName.Length < 3)
                    {
                        exceptions.Add(new ValidationException(106, $"Company Name must be greater than two characters"));
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
