using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Blobs;

public class BlobInfrastructure : IBlobInfrastructure
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobInfrastructure(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task addBlob(IFormFile file, Guid guid, string container)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(container);
        var blobClient = containerClient.GetBlobClient(guid.ToString());
        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }
    }

    public async Task<BlobDownloadInfo> getBlob(Guid guid, string container, string extension)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(container);
        var blobClient = containerClient.GetBlobClient(guid + extension);
        if (!await blobClient.ExistsAsync())
            return null;

        var response = await blobClient.DownloadAsync();
        return response;
    }

    public async Task deleteBlob(Guid guid, string container, string extension)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(container);
        var blobClient = containerClient.GetBlobClient(guid + extension);
        await blobClient.DeleteIfExistsAsync();
    }
}