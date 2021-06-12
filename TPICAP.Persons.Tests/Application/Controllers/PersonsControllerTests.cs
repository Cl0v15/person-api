using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TPICAP.Persons.API;
using Xunit;

namespace TPICAP.Persons.Tests.Application.Controllers
{
    public class PersonsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public PersonsControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _httpClient = fixture.CreateClient();
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_A_200_Ok_Status_When_Persons_Are_Returned()
        {
            // Arrange
            var getJwtResponse = await _httpClient.GetAsync("api/identity");
            var jwt = await getJwtResponse.Content.ReadAsStringAsync();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwt);

            // Act
            var result = await _httpClient.GetAsync("api/persons");

            //Assert
            result.StatusCode.Should().Equals(HttpStatusCode.OK);
            result.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}
