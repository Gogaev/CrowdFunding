using CrowdFundingAPI.Typings;
using Domain.Features.ImageFeatures.Commands;
using Domain.Features.ImageFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reinforced.Typings.Attributes;

namespace CrowdFundingAPI.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/Images")]
[ApiController]
[TsClass(CodeGeneratorType = typeof(AngularControllerGenerator))]
public class ImagesController : ControllerBase
{
    private readonly IMediator _mediator;
    public ImagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("upload")]
    [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
    public async Task<ActionResult<string>> UploadPhoto([FromForm]IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");
        
        var result = Ok(await _mediator.Send(new UploadImageCommand(file)));
        return result;
    }
    
    [HttpGet("{id}")]
    [TsFunction(CodeGeneratorType = typeof(AngularActionCallGenerator))]
    public async Task<IActionResult> GetPhoto(string id)
    {
        var image = await _mediator.Send(new GetImageQuery(id));
        return File(image.ImageData, "image/jpeg");
    }
}