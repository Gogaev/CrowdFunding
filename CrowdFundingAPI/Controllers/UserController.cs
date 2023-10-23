using Core.Dtos.User;
using Domain.DomainModels.Constants;
using Domain.Features.UserFeatures.Commands;
using Domain.Features.UserFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFundingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetUsersQuery());
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = Ok(await _mediator.Send(new GetUserByIdQuery(id)));
            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = (await _mediator.Send(command)).Message?.Split(',');
            
            if(result is null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                token = result[0],
                expired = result[1]
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("User created successfully!");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return Ok("User deleted successfully");
        }
    }
}
