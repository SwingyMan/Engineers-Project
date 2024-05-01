using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IPostsRepository
{
    Task<Post> GetPostByIdAsync(int postId);
    Task<IEnumerable<Post>> GetAllPostsAsync();
    Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);
    Task<IEnumerable<Post>> GetPostsByAvailabilityAsync(string availability);
    Task AddPostAsync(Post post);
    Task UpdatePostAsync(Post post);
    Task DeletePostAsync(int postId);
}
