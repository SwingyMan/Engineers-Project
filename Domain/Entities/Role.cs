using Newtonsoft.Json;

namespace Domain.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public ICollection<User>? Users { get; set; }
}
