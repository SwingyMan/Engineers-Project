using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Friends
{   
    [Key]
    [JsonIgnore]
    public Guid Id { get; set; }
    public Guid UserId1 { get; set; }
    public Guid UserId2 { get; set; }
    public bool Accepted { get; set; } = false;
}