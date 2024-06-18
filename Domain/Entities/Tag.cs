using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Tag
{
    [Key] public Guid Id { get; set; }

    public string TagName { get; set; }

    [JsonIgnore] public IEnumerable<PostsTag>? PostsTags { get; set; }
}