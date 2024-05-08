﻿using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Persistence;

public class SocialPlatformDbContext : DbContext
{
    public SocialPlatformDbContext(DbContextOptions<SocialPlatformDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupUser> GroupUsers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<GroupPost> GroupPosts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostsTag> PostTags { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ChatUser> ChatUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        // User and Role relationship
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        // GroupUser relationships
        modelBuilder.Entity<GroupUser>()
            .HasOne(gu => gu.User)
            .WithMany(u => u.GroupUsers)
            .HasForeignKey(gu => gu.UserId);

        modelBuilder.Entity<GroupUser>()
            .HasOne(gu => gu.Group)
            .WithMany(g => g.GroupUsers)
            .HasForeignKey(gu => gu.GroupId);

        // GroupPost relationships
        modelBuilder.Entity<GroupPost>()
            .HasOne(gp => gp.Group)
            .WithMany(g => g.GroupPosts)
            .HasForeignKey(gp => gp.GroupId);

        modelBuilder.Entity<GroupPost>()
            .HasOne(gp => gp.Post)
            .WithMany(p => p.GroupPosts)
            .HasForeignKey(gp => gp.PostId);

        // PostTag relationships
        modelBuilder.Entity<PostsTag>()
            .HasOne(pt => pt.Post)
            .WithMany(p => p.PostsTags)
            .HasForeignKey(pt => pt.PostId);

        modelBuilder.Entity<PostsTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.PostsTags)
            .HasForeignKey(pt => pt.TagId);

        // ChatUser relationships
        modelBuilder.Entity<ChatUser>() //
        .HasOne(cu => cu.User)
        .WithMany(u => u.ChatUsers)
        .HasForeignKey(cu => cu.UserId);

        modelBuilder.Entity<ChatUser>()
            .HasOne(cu => cu.Chat)
            .WithMany(c => c.ChatUsers)
            .HasForeignKey(cu => cu.ChatId);

        modelBuilder.Entity<ChatUser>()
            .HasOne(cu => cu.Message)
            .WithMany()
            .HasForeignKey(cu => cu.MessageId);

        // Message relationships
        modelBuilder.Entity<Message>()
            .HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserId);
    }
}
