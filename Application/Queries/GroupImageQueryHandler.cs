using System.Net.Mime;
using Infrastructure.Blobs;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Queries;

public class GroupImageQueryHandler : IRequestHandler<GroupImageQuery, FileStreamResult>
{
    private readonly IBlobInfrastructure _blobInfrastructure;
    private readonly SocialPlatformDbContext _socialPlatformDbContext;
    public GroupImageQueryHandler(IBlobInfrastructure blobInfrastructure, SocialPlatformDbContext socialPlatformDbContext)
    {
        _blobInfrastructure = blobInfrastructure;
        _socialPlatformDbContext = socialPlatformDbContext;
    }
    public async Task<FileStreamResult> Handle(GroupImageQuery request, CancellationToken cancellationToken)
    {
        var group = _socialPlatformDbContext.Groups.Single(x => x.Id == request.GroupId);
        var file = await _blobInfrastructure.getBlob(group.ImageLink, "groups");
        return new FileStreamResult(file.Content,file.ContentType)
            {FileDownloadName = group.ImageLink};
    }
}