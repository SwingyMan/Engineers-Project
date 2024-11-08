using Infrastructure.Blobs;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class AttachmentFileQueryHandler : IRequestHandler<AttachmentFileQuery,FileStreamResult>
{
    private readonly SocialPlatformDbContext _context;
    private readonly IBlobInfrastructure _blobInfrastructure;

    public AttachmentFileQueryHandler(SocialPlatformDbContext context, IBlobInfrastructure blobInfrastructure)
    {
        _context = context;
        _blobInfrastructure = blobInfrastructure;
    }
    public async Task<FileStreamResult> Handle(AttachmentFileQuery request, CancellationToken cancellationToken)
    {
        var file = await _context.Attachments.Where(x => x.Id == request.FileID).SingleAsync();
        var data = await _blobInfrastructure.getBlob(file.FileName, file.Type);


        return new FileStreamResult(data.Content,data.ContentType)
            {FileDownloadName = file.FileName};
    }
}