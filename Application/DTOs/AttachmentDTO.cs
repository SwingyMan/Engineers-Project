using Microsoft.AspNetCore.Http;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace Application.DTOs;

public class AttachmentDTO
{
    public IFormFile file { get; set; }
    public Guid PostID { get; set; }
}