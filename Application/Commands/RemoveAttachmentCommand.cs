using MediatR;

namespace Application.Commands;

public class RemoveAttachmentCommand : IRequest
{
    public Guid AttachmentId { get; set; }

    public RemoveAttachmentCommand(Guid attachmentId)
    {
        AttachmentId = attachmentId;
    }
}