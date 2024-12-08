using Domain.Entities;

namespace Domain.Interfaces;

public interface IPostsRepository
{
    Task<Post> GetPostByIdAsync(Guid postId);
    Task<IEnumerable<Post>> GetAllPostsAsync();
    Task<IEnumerable<Post>> GetPostsByUserIdAsync(Guid userId);
    Task<IEnumerable<Post>> GetPostsByAvailabilityAsync(Availability availability);
    Task AddPostAsync(Post post);
    Task<Post> UpdatePostAsync(Guid guid,Post post);
    Task DeletePostAsync(Guid postId);
}