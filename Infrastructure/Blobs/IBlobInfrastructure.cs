using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Blobs;

public interface IBlobInfrastructure
{
    public Task addBlob(IFormFile file, Guid guid, string container);
    public Task<BlobDownloadInfo> getBlob(string name, string container);
    public Task deleteBlob(string name, string container);
}