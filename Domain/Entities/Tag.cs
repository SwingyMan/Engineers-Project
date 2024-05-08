using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Tag
{
    [Key]
    public Guid Id { get; set; }
    public string TagName { get; set; }

    public IEnumerable<PostsTag>? PostsTags { get; set; }
}
