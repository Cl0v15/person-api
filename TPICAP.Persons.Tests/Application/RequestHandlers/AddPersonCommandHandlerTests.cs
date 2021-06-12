using Moq;
using System;
using System.Threading;
using TPICAP.Persons.API.Dtos;
using TPICAP.Persons.API.Requests;
using TPICAP.Persons.API.Requests.Handlers;
using TPICAP.Persons.Domain.Persons;
using Xunit;

namespace TPICAP.Persons.Tests.Application.RequestHandlers
{
    public class AddPersonCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;

        public AddPersonCommandHandlerTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
        }

        [Fact]
        public void Handle_Should_Call_Add_From_IPersonRepository()
        {
            // Arrange
            var sut = new AddPersonCommandHandler(_personRepositoryMock.Object);

            // Act
            sut.Handle(new AddPersonCommand("FirstName", "LastName", new DateTime(1986, 1, 3), SalutationDto.Lord), new CancellationToken());

            // Assert
            _personRepositoryMock.Verify(r => r.Add(It.IsAny<Person>()), Times.Once);
        }
    }
}
