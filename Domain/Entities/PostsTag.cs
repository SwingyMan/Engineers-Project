using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class PostsTag
{
    [Key] public Guid Id { get; set; }

    public Guid PostId { get; set; }
    public Guid TagId { get; set; }

    [JsonIgnore] public Post Post { get; set; }

    [JsonIgnore] public Tag Tag { get; set; }
}