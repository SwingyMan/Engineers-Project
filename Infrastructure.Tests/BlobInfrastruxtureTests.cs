using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Infrastructure.Blobs.Tests
{
    [TestFixture]
    public class BlobInfrastructureTests
    {
        private Mock<BlobServiceClient> _mockBlobServiceClient;
        private Mock<BlobContainerClient> _mockBlobContainerClient;
        private Mock<BlobClient> _mockBlobClient;
        private BlobInfrastructure _blobInfrastructure;

        [SetUp]
        public void Setup()
        {
            _mockBlobServiceClient = new Mock<BlobServiceClient>();
            _mockBlobContainerClient = new Mock<BlobContainerClient>();
            _mockBlobClient = new Mock<BlobClient>();

            _mockBlobServiceClient
                .Setup(x => x.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(_mockBlobContainerClient.Object);

            _mockBlobContainerClient
                .Setup(x => x.GetBlobClient(It.IsAny<string>()))
                .Returns(_mockBlobClient.Object);

            _blobInfrastructure = new BlobInfrastructure(_mockBlobServiceClient.Object);
        }

        [Test]
        public async Task AddBlob_ShouldUploadBlob()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var content = "Hello, World!";
            var fileName = "test.txt";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            mockFile.Setup(_ => _.OpenReadStream()).Returns(ms);
            mockFile.Setup(_ => _.FileName).Returns(fileName);
            mockFile.Setup(_ => _.Length).Returns(ms.Length);

            var guid = Guid.NewGuid();
            var container = "test-container";

            _mockBlobClient
                .Setup(x => x.UploadAsync(It.IsAny<Stream>(), true, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Response.FromValue((BlobContentInfo)null, null));

            // Act
            await _blobInfrastructure.addBlob(mockFile.Object, guid, container);

            // Assert
            _mockBlobContainerClient.Verify(x => x.GetBlobClient(guid.ToString()), Times.Once);
            _mockBlobClient.Verify(x => x.UploadAsync(It.IsAny<Stream>(), true, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetBlob_ShouldReturnBlobDownloadInfo_WhenBlobExists()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var container = "test-container";
            var extension = ".txt";

            var mockBlobDownloadInfo = new Mock<BlobDownloadInfo>();

            _mockBlobClient
                .Setup(x => x.ExistsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Response.FromValue(true, null));

            _mockBlobClient
                .Setup(x => x.DownloadAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Response.FromValue(mockBlobDownloadInfo.Object, null));

            // Act
            var result = await _blobInfrastructure.getBlob(guid+extension, container);

            // Assert
            result.Should().NotBeNull();
            _mockBlobContainerClient.Verify(x => x.GetBlobClient(guid + extension), Times.Once);
            _mockBlobClient.Verify(x => x.ExistsAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockBlobClient.Verify(x => x.DownloadAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetBlob_ShouldReturnNull_WhenBlobDoesNotExist()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var container = "test-container";
            var extension = ".txt";

            _mockBlobClient
                .Setup(x => x.ExistsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Response.FromValue(false, null));

            // Act
            var result = await _blobInfrastructure.getBlob(guid+extension, container);

            // Assert
            result.Should().BeNull();
            _mockBlobContainerClient.Verify(x => x.GetBlobClient(guid + extension), Times.Once);
            _mockBlobClient.Verify(x => x.ExistsAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockBlobClient.Verify(x => x.DownloadAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task DeleteBlob_ShouldDeleteBlob()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var container = "test-container";
            var extension = ".txt";

            _mockBlobClient
                .Setup(x => x.DeleteIfExistsAsync(It.IsAny<DeleteSnapshotsOption>(), It.IsAny<BlobRequestConditions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Response.FromValue(true, null));

            // Act
            await _blobInfrastructure.deleteBlob(guid+extension, container);

            // Assert
            _mockBlobContainerClient.Verify(x => x.GetBlobClient(guid + extension), Times.Once);
            _mockBlobClient.Verify(x => x.DeleteIfExistsAsync(It.IsAny<DeleteSnapshotsOption>(), It.IsAny<BlobRequestConditions>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
