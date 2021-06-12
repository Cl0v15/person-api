using TPICAP.Persons.Domain.Core;

namespace TPICAP.Persons.Domain.Persons
{
    public interface IExistsPersonSpecification : ISpecification<Person> { }

    public class ExistsPersonSpecification :
        Specification<Person>,
        IExistsPersonSpecification
    {
        protected override bool IsSatisfiedBy(Person entity) => entity != null;
    }
}
