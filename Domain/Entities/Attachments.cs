using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Attachments
{
    [Key] public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public string Type { get; set; }
    public string FileName { get; set; }
    public Post Post { get; set; }
}