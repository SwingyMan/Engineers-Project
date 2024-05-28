using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Infrastructure.Translator.TranslatorDTO;
using Newtonsoft.Json;
using RestSharp;

namespace Infrastructure.Translator;

class TranslatorService
{
    public async Task<List<TranslationResult>> Translate(string textToTranslate, string targetLanguage)
    {
        string endpoint = "https://westeurope.api.cognitive.microsoft.com/";
        string location = "westeurope";
        string route = $"/translate?api-version=3.0&to={targetLanguage}";

        var keyvault = new SecretClient(new Uri("https://socialplatformkv.vault.azure.net/"), new DefaultAzureCredential());
        string key = keyvault.GetSecret("translatorkey").Value.Value;


        object[] body = [new { Text = textToTranslate }];
        var requestBody = JsonConvert.SerializeObject(body);

        var client = new RestClient(endpoint);
        var request = new RestRequest(route, Method.Post);

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Ocp-Apim-Subscription-Key", key);
        request.AddHeader("Ocp-Apim-Subscription-Region", location);
        request.AddParameter("application/json", requestBody, ParameterType.RequestBody);

        RestResponse response = await client.ExecuteAsync(request);
        List<TranslationResult>? translationResults = JsonConvert.DeserializeObject<List<TranslationResult>>(response.Content);
        return translationResults;
    }
}
