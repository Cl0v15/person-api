using AutoMapper;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using TPICAP.Persons.API.Dtos;
using TPICAP.Persons.API.Requests;
using TPICAP.Persons.Domain.Persons;
using Xunit;

namespace TPICAP.Persons.Tests.Application.RequestHandlers
{
    public class GetPersonQueryHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetPersonQueryHandler _sut;

        public GetPersonQueryHandlerTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _mapperMock = new Mock<IMapper>();
            _sut = new GetPersonQueryHandler(_personRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Call_GetAllAsync_From_IPersonRepository()
        {
            // Act
            await _sut.Handle(new GetPersonQuery(1), new CancellationToken());

            // Assert
            _personRepositoryMock.Verify(r => r.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Call_Map_From_IMapper()
        {
            // Act
            await _sut.Handle(new GetPersonQuery(1), new CancellationToken());

            // Assert
            _mapperMock.Verify(m => m.Map<PersonDto>(It.IsAny<Person>()), Times.Once);
        }
    }
}
