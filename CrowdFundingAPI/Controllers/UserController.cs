using CrowdFundingAPI.Typings;
using Domain.DomainModels.Constants;
using Domain.Features.UserFeatures.Commands;
using Domain.Features.UserFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reinforced.Typings.Attributes;

namespace CrowdFundingAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    [TsClass(CodeGeneratorType = typeof(AngularControllerGenerator))]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-users")]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetUsersQuery());
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }

        [HttpPost ("login")]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("register")]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize]
        [HttpDelete("{id}")]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return Ok("User deleted successfully");
        }
    }
}
