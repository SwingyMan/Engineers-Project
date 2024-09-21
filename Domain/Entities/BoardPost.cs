using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Domain.Entities;

public class BoardPost
{
    [Key] public int Id { get; set; }

    public int PostsId { get; set; }
    public string Availability { get; set; }

    [JsonIgnore] public Post Post { get; set; }
}