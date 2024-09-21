using Azure;
using Azure.AI.ContentSafety;

namespace Infrastructure.ContentSafety;

public class TextContentSafetyService
{
    private readonly ContentSafetyClient _contentSafetyClient;

    public TextContentSafetyService(ContentSafetyClient contentSafetyClient)
    {
        _contentSafetyClient = contentSafetyClient;
    }

    public Response<AnalyzeTextResult> AnalyzeText(string text)
    {
        var request = new AnalyzeTextOptions(text);

        Response<AnalyzeTextResult> response;
        try
        {
            response = _contentSafetyClient.AnalyzeText(request);
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine("Analyze text failed.\nStatus code: {0}, Error code: {1}, Error message: {2}", ex.Status,
                ex.ErrorCode, ex.Message);
            throw;
        }

        return response;
    }
}