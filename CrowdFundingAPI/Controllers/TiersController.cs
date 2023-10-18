using Core.Dtos;
using Domain.Features.TierFeature.Commands;
using Domain.Features.TierFeature.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFundingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiersController : ControllerBase
    {
        private IMediator _mediator;
        public TiersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateTierCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllTiersQuary()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetTierByIdQuary(id));
            if(result is not null)
            {
                return Ok(result);
            }
            if(result is null)
            {
                return NotFound(new Response { Status = "Error", Message = "Can't find user with this id" });
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteTierCommand(id)));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, UpdateTierCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }
    }
}
