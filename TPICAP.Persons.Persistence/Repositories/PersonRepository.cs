using TPICAP.Persons.Domain.Persons;

namespace TPICAP.Persons.Persistence.Repositories
{
    public class PersonRepository :
        RepositoryBase<Person, AppDbContext>,
        IPersonRepository
    {
        public PersonRepository(AppDbContext entities)
            : base(entities)
        { }
    }
}
