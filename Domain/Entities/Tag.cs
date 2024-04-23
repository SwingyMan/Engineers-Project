namespace Domain.Entities;

public class Tag
{
    public int Id { get; set; }
    public string TagName { get; set; }

    public ICollection<PostsTag> PostsTags { get; set; }
}
