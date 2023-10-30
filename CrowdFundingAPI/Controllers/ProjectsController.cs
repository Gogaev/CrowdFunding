using Domain.DomainModels.Constants;
using Domain.Features.ProjectFeatures.Commands;
using Domain.Features.ProjectFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFundingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-published")]
        public async Task<IActionResult> GetAllPublished()
        {
            return Ok(await _mediator.Send(new GetAllPublishedProjectsQuery()));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllProjectsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _mediator.Send(new GetProjectByIdQuery(id)));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateProjectCommand command)
        {
            await _mediator.Send(command);
            return Ok("Project was created successfully");
        }

        [HttpPost("support")]
        [Authorize]
        public async Task<IActionResult> SupportProject(SupportProjectCommand command)
        {
            await _mediator.Send(command);
            return Ok("Project was supported");
        }

        [Authorize]
        [HttpPatch("publish")]
        public async Task<IActionResult> PublishProject(PublishProjectCommand command)
        {
            await _mediator.Send(command);
            return Ok("Project was published");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteProjectCommand(id));
            return Ok("Project was deleted successfully");
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, UpdateProjectCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);
            return Ok("Project was updated successfully");
        }
    }
}
