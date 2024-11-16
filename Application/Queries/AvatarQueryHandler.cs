using Infrastructure.Blobs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Queries;

public class AvatarQueryHandler : IRequestHandler<AvatarQuery,FileStreamResult>
{
    private readonly IBlobInfrastructure _blobInfrastructure;

    public AvatarQueryHandler(IBlobInfrastructure blobInfrastructure)
    {
        _blobInfrastructure = blobInfrastructure;
    }
    public async Task<FileStreamResult> Handle(AvatarQuery request, CancellationToken cancellationToken)
    {
        var avatar = await _blobInfrastructure.getBlob(request.FileName, "avatars");
        return new FileStreamResult(avatar.Content,avatar.ContentType)
            {FileDownloadName = request.FileName};
    }
}