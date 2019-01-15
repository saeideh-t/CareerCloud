
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        protected IDataRepository<SystemLanguageCodePoco> _repository;
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repository = repository;
        }
        public SystemLanguageCodePoco Get(string Language)
        {
            return _repository.GetSingle(c => c.LanguageID == Language );
        }

        public List<SystemLanguageCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            Add(pocos);
        }
        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            Update(pocos);
        }
      
        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }
        protected void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (SystemLanguageCodePoco item in pocos)
            {
                if(string.IsNullOrEmpty(item.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000, $"Language ID is required."));
                }
                if (string.IsNullOrEmpty(item.Name))
                {
                    exceptions.Add(new ValidationException(1001, $"Code is required."));
                }

                if (string.IsNullOrEmpty(item.NativeName))
                {
                    exceptions.Add(new ValidationException(1002, $"Native Name is required."));
                }

            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}

