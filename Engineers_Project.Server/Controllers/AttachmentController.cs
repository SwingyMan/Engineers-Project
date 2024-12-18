using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;
[Route("api/v1/[controller]/[action]")]
[ApiController]
public class AttachmentController : ControllerBase
{
        private readonly IMediator _mediator;

    public AttachmentController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("{FileId}")]
    public async Task<IActionResult> GetFile(Guid FileId)
    {
        var file = await _mediator.Send(new AttachmentFileQuery(FileId));
        return file;
    }
    [HttpGet("{PostId}")]
    public async Task<IActionResult> GetFileList(Guid PostId)
    {
        var file = await _mediator.Send(new AttachmentQuery(PostId));
        return Ok(file);
    }

    [HttpPost]
    [RequestSizeLimit(50_000_000_000)]

    public async Task<IActionResult> Post([FromForm] AddAttachmentCommand attachmentCommand)
    {
        
        return Ok(await _mediator.Send(attachmentCommand));
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(Guid Id)
    {
        await _mediator.Send(new RemoveAttachmentCommand(Id));
        return Ok();
    }
}