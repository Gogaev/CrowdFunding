using Core.Dtos;
using Domain.DomainModels.Enums;
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
            var result = await _mediator.Send(command);

            if(result.Status == ResponseStatus.InternalServerError)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTiersQuary());

            if(result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetTierByIdQuary(id));

            if (result is null)
            {
                return NotFound(new Response { Status = ResponseStatus.BadRequest, Message = "Can't find user with this id" });
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteTierCommand(id));

            if(result is null)
            {
                return BadRequest();
            }

            switch (result.Status)
            {
                case ResponseStatus.Success:
                    return Ok(result.Message);
                case ResponseStatus.NotFound:
                    return NotFound(result.Message);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, UpdateTierCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }
    }
}
