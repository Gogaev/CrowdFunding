using CrowdFundingAPI.Typings;
using Domain.Features.TierFeature.Commands;
using Domain.Features.TierFeature.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reinforced.Typings.Attributes;

namespace CrowdFundingAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/Tiers")]
    [ApiController]
    [TsClass(CodeGeneratorType = typeof(AngularControllerGenerator))]
    public class TiersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TiersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> Create(CreateTierCommand command)
        {
            await _mediator.Send(command);
            return Ok("Tier was created successfully");
        }

        [HttpGet]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTiersQuery());

            if(result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _mediator.Send(new GetTierByIdQuery(id)));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteTierCommand(id));

            return Ok("Tier was deleted successfully");
        }

        [HttpPut("{id}")]
        [Authorize]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> Update(string id, UpdateTierCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);
            return Ok("Tier was updated successfully");
        }
    }
}
