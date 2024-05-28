using Azure;
using Azure.AI.ContentSafety;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Infrastructure.ContentSafety
{
    public class ImageContentSafetyService
    {
        private readonly ContentSafetyClient _contentSafetyClient;
        public ImageContentSafetyService(ContentSafetyClient contentSafetyClient)
        {
            _contentSafetyClient = contentSafetyClient;
        }
        public Response<AnalyzeImageResult> AnalyzeImage(string imagePath)
        {
            ContentSafetyImageData image = new ContentSafetyImageData(BinaryData.FromBytes(File.ReadAllBytes(imagePath)));

            var request = new AnalyzeImageOptions(image);

            Response<AnalyzeImageResult> response;
            try
            {
                response = _contentSafetyClient.AnalyzeImage(request);
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine("Analyze image failed.\nStatus code: {0}, Error code: {1}, Error message: {2}", ex.Status, ex.ErrorCode, ex.Message);
                throw;
            }

            return response;
        }
    }
}
