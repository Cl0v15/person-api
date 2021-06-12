using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TPICAP.Persons.Persistence;

namespace TPICAP.Persons.API.Core
{
    [Route("api/[controller]")]
    public class ApiControllerBase : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public ApiControllerBase(
            IMediator mediator,
            IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        protected async Task<IActionResult> Ok<TResponse>(IRequest<TResponse> request)
        {
            return base.Ok(await _mediator.Send(request));
        }

        protected async Task<IActionResult> NoContent<TResponse>(IRequest<TResponse> request)
        {
            await _mediator.Send(request);
            await _unitOfWork.SaveChangesAsync();
            return base.NoContent();
        }
    }
}
