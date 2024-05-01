using System.Collections.Generic;

namespace Domain.Entities;

public class Tag
{
    public int Id { get; set; }
    public string TagName { get; set; }

    public IEnumerable<PostsTag>? PostsTags { get; set; }
}
