using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AddAttachmentCommand : IRequest<Attachments>
{
    public AttachmentDTO AttachmentDto { get; set; }
}