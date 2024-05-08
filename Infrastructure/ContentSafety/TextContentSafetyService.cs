using Azure;
using Azure.AI.ContentSafety;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Infrastructure.ContentSafety
{
    public class TextContentSafetyService
    {
        public Response<AnalyzeTextResult> AnalyzeText(string text)
        {
            var keyvault = new SecretClient(new Uri("https://socialplatformkv.vault.azure.net/"), new DefaultAzureCredential());
            string endpoint = "https://westeurope.api.cognitive.microsoft.com/";
            string key = keyvault.GetSecret("storagekey").Value.Value;
            ContentSafetyClient client = new ContentSafetyClient(new Uri(endpoint), new AzureKeyCredential(key));

            var request = new AnalyzeTextOptions(text);

            Response<AnalyzeTextResult> response;
            try
            {
                response = client.AnalyzeText(request);
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine("Analyze text failed.\nStatus code: {0}, Error code: {1}, Error message: {2}", ex.Status, ex.ErrorCode, ex.Message);
                throw;
            }
            return response;
        }
    }
}
