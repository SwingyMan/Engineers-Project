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

    public async Task addBlob(IFormFile file, Guid guid)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("images");
        BlobClient blobClient = containerClient.GetBlobClient(guid.ToString());
        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }
    }

    public async Task<BlobDownloadInfo> getBlob(Guid guid)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("tasks");
        BlobClient blobClient = containerClient.GetBlobClient(guid + ".zip");
        if (!await blobClient.ExistsAsync())
            return null;

        var response = await blobClient.DownloadAsync();
        return response;
    }

    public async Task deleteBlob(Guid guid)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("tasks");
        BlobClient blobClient = containerClient.GetBlobClient(guid + ".zip");
        await blobClient.DeleteIfExistsAsync();
    }
}


