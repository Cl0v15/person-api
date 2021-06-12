using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TPICAP.Persons.API.Dtos;
using TPICAP.Persons.API.Requests;
using TPICAP.Persons.Domain.Persons;
using Xunit;

namespace TPICAP.Persons.Tests.Application.RequestHandlers
{
    public class UpdatePersonCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly Mock<IExistsPersonSpecification> _existsPersonSpecificationMock;
        private readonly UpdatePersonCommandHandler _sut;

        public UpdatePersonCommandHandlerTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _existsPersonSpecificationMock = new Mock<IExistsPersonSpecification>();
            _sut = new UpdatePersonCommandHandler(_personRepositoryMock.Object, _existsPersonSpecificationMock.Object);

            _personRepositoryMock.Setup(r => r
                .GetAsync(It.IsAny<int>()))
                .ReturnsAsync(Person.New(Name.New("FirstName1"), Name.New("LastName1"), BirthDate.New(new DateTime(1962, 5, 10)), Salutation.Mr));
        }

        [Fact]
        public async Task Handle_Calls_GetAsync_From_IPersonRepository_To_Retrieve_The_Person_To_Delete()
        {
            // Act
            await _sut.Handle(NewUpdatePersonCommand(), new CancellationToken());

            // Assert
            _personRepositoryMock.Verify(r => r.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Calls_EnforceRule_From_IExistsPersonSpecification()
        {
            // Act
            await _sut.Handle(NewUpdatePersonCommand(), new CancellationToken());

            // Assert
            _existsPersonSpecificationMock.Verify(s => s.EnforceRule(It.IsAny<Person>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Calls_Delete_From_IPersonRepository_To_Delete_The_Person()
        {
            // Act
            await _sut.Handle(NewUpdatePersonCommand(), new CancellationToken());

            // Assert
            _personRepositoryMock.Verify(r => r.Update(It.IsAny<Person>()), Times.Once);
        }

        private static UpdatePersonCommand NewUpdatePersonCommand() =>
            new UpdatePersonCommand(1, "FirstNameUpdated", "LastNameUpdated", new DateTime(1982, 5, 10), SalutationDto.Capt);
    }
}
