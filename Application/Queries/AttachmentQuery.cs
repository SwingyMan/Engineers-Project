using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Queries;

public class AttachmentQuery :  IRequest<List<Attachments>>
{
    public Guid PostId { get; set; }

    public AttachmentQuery(Guid postId)
    {
        PostId = postId;
    }
}