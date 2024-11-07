using AutoMapper;
using Domain.Entities;
using Infrastructure.Blobs;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class AttachmentQueryHandler : IRequestHandler<AttachmentQuery, List<Attachments>>
{
    private readonly SocialPlatformDbContext _context;

    public AttachmentQueryHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<List<Attachments>> Handle(AttachmentQuery request, CancellationToken cancellationToken)
    {
        var attachments = await _context.Attachments.Where(x => x.PostId == request.PostId).ToListAsync();
        return attachments;
    }
}