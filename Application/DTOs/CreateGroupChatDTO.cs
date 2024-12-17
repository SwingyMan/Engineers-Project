using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Application.DTOs;

public class CreateGroupChatDTO
{
    public Guid[] usersGuids {  get; set; } = Array.Empty<Guid>();
    public string Name { get; set; } = "";
}
