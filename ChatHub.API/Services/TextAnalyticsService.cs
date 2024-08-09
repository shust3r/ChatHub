using Azure;
using Azure.AI.TextAnalytics;

namespace ChatHub.API.Services;

public class TextAnalyticsService
{
    private readonly TextAnalyticsClient _textAnalyticsClient 
        = new TextAnalyticsClient(
            new Uri("put_your_api_URI_here"),
            new AzureKeyCredential("put_your_key_here"));

    public async Task<DocumentSentiment> EvaluateMessage(string message)
    {
        var response = await _textAnalyticsClient.AnalyzeSentimentAsync(message);

        return response.Value;
    }

    public string AddEmoji(string sentiment)
    {
        return sentiment == "Positive" ? "😊" :
            sentiment == "Negative" ? "😒" : "😐";
    }
}
