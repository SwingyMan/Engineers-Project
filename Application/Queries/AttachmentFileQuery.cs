using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Queries;

public class AttachmentFileQuery : IRequest<FileStreamResult>
{
    public Guid FileID { get; set; }

    public AttachmentFileQuery(Guid fileId)
    {
        FileID = fileId;
    }
}