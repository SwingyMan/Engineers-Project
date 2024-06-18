using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Blobs;

public interface IBlobInfrastructure
{
    public Task addBlob(IFormFile file, Guid guid, string container);
    public Task<BlobDownloadInfo> getBlob(Guid guid, string container, string extension);
    public Task deleteBlob(Guid guid, string container, string extension);
}