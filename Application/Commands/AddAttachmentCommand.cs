using Application.DTOs;
using MediatR;

namespace Application.Commands;

public class AddAttachmentCommand : IRequest
{
    public AttachmentDTO AttachmentDto { get; set; }
}