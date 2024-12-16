namespace Application.DTOs;

public class GroupUpdateDTO
{
    public Guid GroupID { get; set; }
    public string GroupName { get; set; }
    public string GroupDescription { get; set; }
}