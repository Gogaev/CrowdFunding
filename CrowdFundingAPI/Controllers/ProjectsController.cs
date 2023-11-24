using Core.Dtos.Project;
using CrowdFundingAPI.Typings;
using Domain.DomainModels.Constants;
using Domain.DomainModels.Enums;
using Domain.Features.ProjectFeatures.Commands;
using Domain.Features.ProjectFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reinforced.Typings.Attributes;

namespace CrowdFundingAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/Projects")]
    [ApiController]
    [TsClass(CodeGeneratorType = typeof(AngularControllerGenerator))]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-published")]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<ActionResult<IEnumerable<PublishedProjectDto>>> GetAllPublished()
        {
            return Ok(await _mediator.Send(new GetAllPublishedProjectsQuery()));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("get-all")]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllProjectsQuery()));
        }
        
        [Authorize]
        [HttpGet("get-created/{status}")]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<ActionResult<IEnumerable<ProjectWithTiersDto>>> GetAllByUser(Status status)
        {
            Console.WriteLine(status);
            return Ok(await _mediator.Send(new GetAllCreatedProjectsQuery(status)));
        }

        [Authorize]
        [HttpGet("get-filtered")]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<ActionResult<IEnumerable<ProjectWithTiersDto>>> GetFiltered()
        {
            throw new Exception();
        }
        
        [HttpGet("{id}")]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<ActionResult<ProjectWithTiersDto>> GetById(string id)
        {
            return Ok(await _mediator.Send(new GetProjectByIdQuery(id)));
        }

        [HttpPost]
        [Authorize]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> Create(CreateProjectCommand command)
        {
            await _mediator.Send(command);
            return Ok("Project was created successfully");
        }

        [HttpPost("support")]
        [Authorize]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> SupportProject(SupportProjectCommand command)
        {
            await _mediator.Send(command);
            return Ok("Project was supported");
        }

        [Authorize]
        [HttpPatch("publish")]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> PublishProject(PublishProjectCommand command)
        {
            await _mediator.Send(command);
            return Ok("Project was published");
        }

        [HttpDelete("{id}")]
        [Authorize]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteProjectCommand(id));
            return Ok("Project was deleted successfully");
        }

        [HttpPut("{id}")]
        [Authorize]
        [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
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
