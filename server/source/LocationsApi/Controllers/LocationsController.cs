using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using LocationsApi.Service.Features.LocationFeatures.Commands;
using LocationsApi.Service.Features.LocationFeatures.Queries;
using System.Threading.Tasks;

namespace LocationsApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Locations")]
    [ApiVersion("1.0")]
    public class LocationsController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<IActionResult> Create(CreateLocationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllLocationsQuery()));
        }

        [HttpGet("{query}")]        
        public async Task<IActionResult> SearchForPlace(string query)
        {
            return Ok(await Mediator.Send(new GetAllLocationsQuery { Query = query }));
        }

    }
}
