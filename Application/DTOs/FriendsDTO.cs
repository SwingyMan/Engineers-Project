using Domain.Entities;

namespace Application.DTOs;

public class FriendsDTO
{
    public List<User> Sent { get; set; } 
    public List<User> Received { get; set; }
    public List<User> Friends { get; set; }

    public FriendsDTO(List<User> sent, List<User> received, List<User> friends)
    {
        Sent = sent;
        Received = received;
        Friends = friends;
    }
}