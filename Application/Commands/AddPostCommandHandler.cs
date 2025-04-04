﻿using AutoMapper;
using Azure.AI.ContentSafety;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands;

public class AddPostCommandHandler : IRequestHandler<AddPostCommand, Post>
{
    private readonly IGenericRepository<Post> _genericRepository;
    private readonly IMapper _mapper;
    private readonly ContentSafetyClient _safetyClient;
    private readonly SocialPlatformDbContext _context;
    public AddPostCommandHandler(IGenericRepository<Post> genericRepository, IMapper mapper,ContentSafetyClient safetyClient,SocialPlatformDbContext context)
    {
        _context = context;
        _genericRepository = genericRepository;
        _mapper = mapper;
        _safetyClient = safetyClient;
    }

    public async Task<Post> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        var postEntity = _mapper.Map<Post>(request.entity);
        postEntity.UserId = request.guid;
        postEntity.CreatedAt = DateTime.Now;
        postEntity.Status = "status";
        postEntity.Availability = request.entity.Availability;
        var user = _context.Users.Single(x=>x.Id == request.guid);
        postEntity.User = user;
        if (await CheckText(postEntity.Body))
        {
            if (postEntity.Availability == Availability.Group)
            {
                var post =await _genericRepository.Add(postEntity);
                var groupPost = new GroupPost(request.entity.GroupId,post.Id );
                _context.GroupPosts.Add(groupPost);
                await _context.SaveChangesAsync(cancellationToken);
                return post;
            }
            return await _genericRepository.Add(postEntity);

        }

        return null;
    }

    private async Task<bool> CheckText(string text)
    {
        try
        {
            // Analyze text
            var response = await _safetyClient.AnalyzeTextAsync(text);

            // Check if any content category flags the text as inappropriate
            if (response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == TextCategory.Hate)?.Severity > 2 ||
                response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == TextCategory.SelfHarm)?.Severity > 0 ||
                response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == TextCategory.Sexual)?.Severity > 0 || 
                response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == TextCategory.Violence)?.Severity > 0)
            {
                return false; // Inappropriate content detected
            }

            return true; // Content is safe
        }
        catch (Exception ex)
        {
            return false; // Treat as unsafe if error occurs
        }
    }
}
