using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Queries;

public class AvatarQuery : IRequest<FileStreamResult>
{
    public string FileName { get; set; }

    public AvatarQuery(string fileName)
    {
        FileName = fileName;
    }
}