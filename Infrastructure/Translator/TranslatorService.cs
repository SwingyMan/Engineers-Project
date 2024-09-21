using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Infrastructure.Translator.TranslatorDTO;
using Newtonsoft.Json;
using RestSharp;

namespace Infrastructure.Translator;

internal class TranslatorService
{
    public async Task<List<TranslationResult>> Translate(string textToTranslate, string targetLanguage)
    {
        var endpoint = "https://westeurope.api.cognitive.microsoft.com/";
        var location = "westeurope";
        var route = $"/translate?api-version=3.0&to={targetLanguage}";

        var keyvault = new SecretClient(new Uri("https://socialplatformkv.vault.azure.net/"),
            new DefaultAzureCredential());
        var key = keyvault.GetSecret("translatorkey").Value.Value;


        object[] body = [new { Text = textToTranslate }];
        var requestBody = JsonConvert.SerializeObject(body);

        var client = new RestClient(endpoint);
        var request = new RestRequest(route, Method.Post);

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Ocp-Apim-Subscription-Key", key);
        request.AddHeader("Ocp-Apim-Subscription-Region", location);
        request.AddParameter("application/json", requestBody, ParameterType.RequestBody);

        var response = await client.ExecuteAsync(request);
        var translationResults = JsonConvert.DeserializeObject<List<TranslationResult>>(response.Content);
        return translationResults;
    }
}