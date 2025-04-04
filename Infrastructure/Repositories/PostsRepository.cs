﻿using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostsRepository(SocialPlatformDbContext _context) : IPostsRepository
{
    public async Task<Post> GetPostByIdAsync(Guid postId)
    {
        return await _context.Set<Post>().FindAsync(postId);
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return await _context.Set<Post>().ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(Guid userId)
    {
        return await _context.Set<Post>()
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPostsByAvailabilityAsync(Availability availability)
    {
        return await _context.Set<Post>()
            .Where(p => p.Availability == availability)
            .ToListAsync();
    }

    public async Task AddPostAsync(Post post)
    {
        _context.Set<Post>().Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task<Post> UpdatePostAsync(Guid guid,Post post)
    {
        var entity = await _context.Posts.FindAsync(guid);
        entity.Availability = post.Availability;
        entity.Title = post.Title;
        entity.Body = post.Body;
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeletePostAsync(Guid postId)
    {
        var post = await _context.Set<Post>().FindAsync(postId);
        if (post != null)
        {
            _context.Set<Post>().Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}