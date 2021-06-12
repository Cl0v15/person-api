using FluentValidation;
using Newtonsoft.Json;

namespace TPICAP.Persons.Domain.Core
{
    public interface ISpecification<TEntity>
    {
        void EnforceRule(TEntity entity, string errorMessage);
    }

    public abstract class Specification<TEntity> : ISpecification<TEntity>
    {
        protected abstract bool IsSatisfiedBy(TEntity entity);

        public virtual void EnforceRule(TEntity entity, string errorMessage)
        {
            if (!IsSatisfiedBy(entity))
            {
                throw new ValidationException(
                    JsonConvert.SerializeObject(new
                    {
                        errorMessage = errorMessage
                    })
               );
            }
        }
    }
}
