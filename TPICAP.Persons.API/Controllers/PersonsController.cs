using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TPICAP.Persons.API.Core;
using TPICAP.Persons.API.Dtos;
using TPICAP.Persons.API.Requests;
using TPICAP.Persons.Persistence;

namespace TPICAP.Persons.API.Controllers
{
    public class PersonsController : ApiControllerBase
    {
        public PersonsController(
            IMediator mediator,
            IUnitOfWork unitOfWork)
            : base(mediator, unitOfWork)
        { }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            return await Ok(new GetAllPersonsQuery());
        }

        [HttpGet("{personId:int}")]
        [Authorize]
        public async Task<IActionResult> GetAsync([FromRoute] int personId)
        {
            return await Ok(new GetPersonQuery(personId));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAsync([FromBody] NewPersonDto person)
        {
            return await NoContent(new AddPersonCommand(
                person.FirstName,
                person.LastName,
                person.DateOfBirth,
                person.Salutation));
        }

        [HttpPut("{personId:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromRoute] int personId, [FromBody] PersonDto person)
        {
            return await NoContent(new UpdatePersonCommand(
                personId,
                person.FirstName,
                person.LastName,
                person.DateOfBirth,
                person.Salutation));
        }

        [HttpDelete("{personId:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync([FromRoute] int personId)
        {
            return await NoContent(new DeletePersonCommand(personId));
        }
    }
}
