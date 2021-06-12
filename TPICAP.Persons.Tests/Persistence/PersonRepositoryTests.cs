using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPICAP.Persons.Domain.Persons;
using TPICAP.Persons.Persistence;
using TPICAP.Persons.Persistence.Repositories;
using Xunit;

namespace TPICAP.Persons.Tests.Persistence
{
    public class PersonRepositoryTests
    {
        private readonly InMemoryDbContext _inMemoryDbContext;
        private readonly AppDbContext _appDbContext;

        public PersonRepositoryTests()
        {
            _inMemoryDbContext = new InMemoryDbContext("AppDbContextPersonTest");
            _appDbContext = _inMemoryDbContext.GetDbContext();
        }

        [Fact]
        public void Add_Should_Add_A_New_Person_Into_The_DB_Context()
        {
            // Arrange
            var sut = new PersonRepository(_appDbContext);
            var personToAdd = Person.New(Name.New("FirstName"), Name.New("LastName"), BirthDate.New(new System.DateTime(1962, 5, 10)), Salutation.Mr);

            // Act
            sut.Add(personToAdd);

            // Assert
            var addedPerson = _appDbContext.Person.Find(personToAdd.Id);

            addedPerson.Should().NotBeNull();
            addedPerson.Should().BeOfType<Person>();

            _appDbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_The_Persons_From_The_DB_Context()
        {
            // Arrange
            _inMemoryDbContext.AddEntities(new List<Person>
            {
                Person.New(Name.New("FirstName1"), Name.New("LastName1"), BirthDate.New(new System.DateTime(1962, 5, 10)), Salutation.Mr),
                Person.New(Name.New("FirstName2"), Name.New("LastName2"), BirthDate.New(new System.DateTime(1952, 6, 11)), Salutation.Miss),
                Person.New(Name.New("FirstName3"), Name.New("LastName3"), BirthDate.New(new System.DateTime(1942, 7, 12)), Salutation.Mrs)
            });

            var sut = new PersonRepository(_appDbContext);

            // Act
            var persons = await sut.GetAllAsync();

            // Assert
            persons.Should().NotBeEmpty();
            persons.Should().AllBeOfType<Person>();
            persons.Should().HaveCount(3);

            _appDbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetAsync_Should_Return_The_Person_With_The_Given_Id_From_The_DB_Context()
        {
            // Arrange
            var personToRetrieve = Person.New(Name.New("FirstName1"), Name.New("LastName1"), BirthDate.New(new System.DateTime(1962, 5, 10)), Salutation.Mr);

            _inMemoryDbContext.AddEntities(new List<Person>
            {
                personToRetrieve,
                Person.New(Name.New("FirstName2"), Name.New("LastName2"), BirthDate.New(new System.DateTime(1952, 6, 11)), Salutation.Miss),
                Person.New(Name.New("FirstName3"), Name.New("LastName3"), BirthDate.New(new System.DateTime(1942, 7, 12)), Salutation.Mrs)
            });

            var sut = new PersonRepository(_appDbContext);

            // Act
            var person = await sut.GetAsync(personToRetrieve.Id);

            // Assert
            person.Should().NotBeNull();
            person.Should().BeOfType<Person>();

            _appDbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void Update_Should_Update_The_Person_From_The_DB_Context()
        {
            // Arrange
            var personToUpdate = Person.New(Name.New("FirstName1"), Name.New("LastName1"), BirthDate.New(new System.DateTime(1962, 5, 10)), Salutation.Mr);
            var updatedFirstName = Name.New("FirstNameUpdated");
            var updatedLastName = Name.New("lastNameUpdated");
            var updatedDateOfBirth = BirthDate.New(new System.DateTime(1982, 2, 1));
            var updatedSalutation = Salutation.Major;

            _inMemoryDbContext.AddEntities(new List<Person>
            {
                personToUpdate,
                Person.New(Name.New("FirstName2"), Name.New("LastName2"), BirthDate.New(new System.DateTime(1952, 6, 11)), Salutation.Miss),
                Person.New(Name.New("FirstName3"), Name.New("LastName3"), BirthDate.New(new System.DateTime(1942, 7, 12)), Salutation.Mrs)
            });

            var sut = new PersonRepository(_appDbContext);

            // Act
            personToUpdate.Update(updatedFirstName, updatedLastName, updatedDateOfBirth, updatedSalutation);
            sut.Update(personToUpdate);
            _appDbContext.SaveChanges();

            // Assert
            personToUpdate.FirstName.Should().Be(updatedFirstName);
            personToUpdate.LastName.Should().Be(updatedLastName);
            personToUpdate.DateOfBirth.Should().Be(updatedDateOfBirth);
            personToUpdate.Salutation.Should().Be(updatedSalutation);

            _appDbContext.Database.EnsureDeleted();
        }

        [Fact]
        public void Delete_Should_Remove_The_Person_From_The_DB_Context()
        {
            // Arrange
            var personToDelete = Person.New(Name.New("FirstName1"), Name.New("LastName1"), BirthDate.New(new System.DateTime(1962, 5, 10)), Salutation.Mr);

            _inMemoryDbContext.AddEntities(new List<Person>
            {
                personToDelete,
                Person.New(Name.New("FirstName2"), Name.New("LastName2"), BirthDate.New(new System.DateTime(1952, 6, 11)), Salutation.Miss),
                Person.New(Name.New("FirstName3"), Name.New("LastName3"), BirthDate.New(new System.DateTime(1942, 7, 12)), Salutation.Mrs)
            });

            var sut = new PersonRepository(_appDbContext);

            // Act
            sut.Delete(personToDelete);
            _appDbContext.SaveChanges();

            // Assert
            var deletedPerson = _appDbContext.Person.Find(personToDelete.Id);

            deletedPerson.Should().BeNull();

            _appDbContext.Database.EnsureDeleted();
        }
    }
}
