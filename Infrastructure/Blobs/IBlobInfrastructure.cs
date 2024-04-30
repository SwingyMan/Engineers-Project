using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Blobs;
public interface IBlobInfrastructure
{
    public Task addBlob(IFormFile file, Guid guid);
    public Task<BlobDownloadInfo> getBlob(Guid guid);
    public Task deleteBlob(Guid guid);
}
