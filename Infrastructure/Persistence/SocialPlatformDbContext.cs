using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Persistence;

internal class SocialPlatformDbContext(DbContextOptions<SocialPlatformDbContext> dbContextOptions)
    : IdentityDbContext<User>(dbContextOptions)
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupUser> GroupUsers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<GroupPost> GroupPosts { get; set; }
    //public DbSet<BoardPost> BoardPosts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostsTag> PostsTags { get; set; }
    public DbSet<GroupChat> GroupChats { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ChatUser> ChatUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        // User & GroupUser
        modelBuilder.Entity<GroupUser>()
            .HasKey(gu => new { gu.UserId, gu.GroupId });
        modelBuilder.Entity<GroupUser>()
            .HasOne(gu => gu.User)
            .WithMany(u => u.GroupUsers)
            .HasForeignKey(gu => gu.UserId);
        modelBuilder.Entity<GroupUser>()
            .HasOne(gu => gu.Group)
            .WithMany(g => g.GroupUsers)
            .HasForeignKey(gu => gu.GroupId);

        // Group & GroupPost 
        modelBuilder.Entity<GroupPost>()
            .HasKey(gp => new { gp.GroupId, gp.PostId });
        modelBuilder.Entity<GroupPost>()
            .HasOne(gp => gp.Group)
            .WithMany(g => g.GroupPosts)
            .HasForeignKey(gp => gp.GroupId);
        modelBuilder.Entity<GroupPost>()
            .HasOne(gp => gp.Post)
            .WithMany(p => p.GroupPosts)
            .HasForeignKey(gp => gp.PostId);

        // User & Post 
        modelBuilder.Entity<Post>()
            .HasOne<User>()
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId);

        /*// Post & BoardPost  
        modelBuilder.Entity<BoardPost>()
            .HasOne(bp => bp.Post)
            .WithOne(p => p.BoardPost) // ??
            .HasForeignKey<BoardPost>(bp => bp.PostsId);*/

        // Post & PostsTag 
        modelBuilder.Entity<PostsTag>()
            .HasKey(pt => new { pt.PostId, pt.TagId });
        modelBuilder.Entity<PostsTag>()
            .HasOne(pt => pt.Post)
            .WithMany(p => p.PostsTags)
            .HasForeignKey(pt => pt.PostId);
        modelBuilder.Entity<PostsTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.PostsTags)
            .HasForeignKey(pt => pt.TagId);

        // GroupChat & ChatMessage 
        modelBuilder.Entity<ChatMessage>()
            .HasKey(cm => new { cm.ChatId, cm.MessageId });
        modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.GroupChat)
            .WithMany(gc => gc.ChatMessages)
            .HasForeignKey(cm => cm.ChatId);
        modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.Message)
            .WithOne()
            .HasForeignKey<ChatMessage>(cm => cm.MessageId);

        // User & ChatUser 
        modelBuilder.Entity<ChatUser>()
            .HasKey(cu => new { cu.FirstUserId, cu.SecondUserId });
        modelBuilder.Entity<ChatUser>()
            .HasOne(cu => cu.FirstUser)
            .WithMany()
            .HasForeignKey(cu => cu.FirstUserId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<ChatUser>()
            .HasOne(cu => cu.SecondUser)
            .WithMany()
            .HasForeignKey(cu => cu.SecondUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
