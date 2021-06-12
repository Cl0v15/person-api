using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPICAP.Persons.API.Dtos;
using TPICAP.Persons.API.Requests;
using TPICAP.Persons.Domain.Persons;
using Xunit;

namespace TPICAP.Persons.Tests.Application.RequestHandlers
{
    public class GetAllPersonsQueryHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetAllPersonsQueryHandler _sut;

        public GetAllPersonsQueryHandlerTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _mapperMock = new Mock<IMapper>();
            _sut = new GetAllPersonsQueryHandler(_personRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Call_GetAllAsync_From_IPersonRepository()
        {
            // Act
            await _sut.Handle(new GetAllPersonsQuery(), new CancellationToken());

            // Assert
            _personRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Call_Map_From_IMapper()
        {
            // Act
            await _sut.Handle(new GetAllPersonsQuery(), new CancellationToken());

            // Assert
            _mapperMock.Verify(m => m.Map<IEnumerable<PersonDto>>(It.IsAny<IEnumerable<Person>>()), Times.Once);
        }
    }
}
