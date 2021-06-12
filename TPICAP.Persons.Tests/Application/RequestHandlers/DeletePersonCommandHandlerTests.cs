using Moq;
using System.Threading;
using System.Threading.Tasks;
using TPICAP.Persons.API.Requests;
using TPICAP.Persons.API.Requests.Handlers;
using TPICAP.Persons.Domain.Persons;
using Xunit;

namespace TPICAP.Persons.Tests.Application.RequestHandlers
{
    public class DeletePersonCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly Mock<IExistsPersonSpecification> _existsPersonSpecificationMock;

        public DeletePersonCommandHandlerTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _existsPersonSpecificationMock = new Mock<IExistsPersonSpecification>();
        }

        [Fact]
        public async Task Handle_Calls_GetAsync_From_IPersonRepository_To_Retrieve_The_Person_To_Delete()
        {
            // Arrange
            var sut = new DeletePersonCommandHandler(
                _personRepositoryMock.Object, _existsPersonSpecificationMock.Object);

            // Act
            await sut.Handle(new DeletePersonCommand(It.IsAny<int>()), new CancellationToken());

            // Assert
            _personRepositoryMock.Verify(r => r.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Calls_EnforceRule_From_IExistsPersonSpecification()
        {
            // Arrange
            var sut = new DeletePersonCommandHandler(
                _personRepositoryMock.Object, _existsPersonSpecificationMock.Object);

            // Act
            await sut.Handle(new DeletePersonCommand(It.IsAny<int>()), new CancellationToken());

            // Assert
            _existsPersonSpecificationMock.Verify(s => s.EnforceRule(It.IsAny<Person>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Calls_Delete_From_IPersonRepository_To_Delete_The_Person()
        {
            // Arrange
            var sut = new DeletePersonCommandHandler(
                _personRepositoryMock.Object, _existsPersonSpecificationMock.Object);

            // Act
            await sut.Handle(new DeletePersonCommand(It.IsAny<int>()), new CancellationToken());

            // Assert
            _personRepositoryMock.Verify(r => r.Delete(It.IsAny<Person>()), Times.Once);
        }
    }
}
