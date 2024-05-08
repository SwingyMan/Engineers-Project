using Azure;
using Azure.AI.ContentSafety;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Infrastructure.ContentSafety
{
    public class ImageContentSafetyService
    {
        public Response<AnalyzeImageResult> AnalyzeImage(string imagePath)
        {
            var keyvault = new SecretClient(new Uri("https://socialplatformkv.vault.azure.net/"), new DefaultAzureCredential());
            string endpoint = "https://westeurope.api.cognitive.microsoft.com/";
            string key = keyvault.GetSecret("storagekey").Value.Value;
            ContentSafetyClient client = new ContentSafetyClient(new Uri(endpoint), new AzureKeyCredential(key));

            ContentSafetyImageData image = new ContentSafetyImageData(BinaryData.FromBytes(File.ReadAllBytes(imagePath)));

            var request = new AnalyzeImageOptions(image);

            Response<AnalyzeImageResult> response;
            try
            {
                response = client.AnalyzeImage(request);
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
