using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Domain.Entities;

public class GroupPost
{
    [Key]
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public Guid PostId { get; set; }
    [JsonIgnore]
    public Group Group { get; set; }
    [JsonIgnore]
    public Post Post { get; set; }
}
