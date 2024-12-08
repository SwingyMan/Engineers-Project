using Domain.Entities;
using Infrastructure.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence;

public class SocialPlatformDbContext : DbContext
{
    public SocialPlatformDbContext()
    {

    }
    public SocialPlatformDbContext(DbContextOptions<SocialPlatformDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateInfinityConversions", true);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupUser> GroupUsers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<GroupPost> GroupPosts { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ChatUser> ChatUsers { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Attachments> Attachments { get; set; }
    public DbSet<Friends> Friends { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
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

        // ChatUser relationships
        //modelBuilder.Entity<ChatUser>() 
        //    .HasOne(cu => cu.User)
        //    .WithMany(u => u.ChatUsers)
        //    .HasForeignKey(cu => cu.UserId);

        //modelBuilder.Entity<User>()
        //    .HasOne(cu => cu.ChatUsers)
        //    .WithMany(c => c)
        //    .HasForeignKey(cu => cu.ChatId);

        //modelBuilder.Entity<ChatUser>()
        //    .HasOne(cu => cu.Message)
        //    .WithMany()
        //    .HasForeignKey(cu => cu.MessageId);

        // Message relationships
        modelBuilder.Entity<Message>()
            .HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserId);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatId);

        // Chat relationships
        modelBuilder.Entity<Chat>()
            .HasMany(c => c.Users)
            .WithMany(u => u.Chats)
            .UsingEntity<Dictionary<string, object>>(
            "ChatUser", 
            j => j.HasOne<User>().WithMany().HasForeignKey("UserId"), 
            j => j.HasOne<Chat>().WithMany().HasForeignKey("ChatId"));
    

    // Posts relationships
    modelBuilder.Entity<Post>()
            .HasMany(x => x.Attachments)
            .WithOne(p => p.Post)
            .HasForeignKey(x=>x.PostId);

    }
}