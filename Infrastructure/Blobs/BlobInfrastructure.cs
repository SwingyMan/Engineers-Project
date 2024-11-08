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
        var blobClient = containerClient.GetBlobClient($"{guid.ToString()}{Path.GetExtension(file.FileName)}");
        var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream, true);
    }

    public async Task<BlobDownloadInfo> getBlob(string name, string container)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(container);
        var blobClient = containerClient.GetBlobClient(name);
        if (!await blobClient.ExistsAsync())
            return null;

        var response = await blobClient.DownloadAsync();
        return response;
    }

    public async Task deleteBlob(string name, string container)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(container);
        var blobClient = containerClient.GetBlobClient(name);
        await blobClient.DeleteIfExistsAsync();
    }
}