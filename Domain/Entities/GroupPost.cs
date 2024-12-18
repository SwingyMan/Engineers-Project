using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Domain.Entities;

public class GroupPost
{
    [Key] public Guid Id { get; set; }

    public Guid GroupId { get; set; }
    public Guid PostId { get; set; }

    [JsonIgnore] public Group Group { get; set; }

    [JsonIgnore] public Post Post { get; set; }

    public GroupPost(Guid groupId, Guid postId)
    {
        Id = new Guid();
        GroupId = groupId;
        PostId = postId;
    }
}