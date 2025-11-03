using System.ClientModel;
using DuckCreek.ComandLine.Tool.Clients.Settings;
using Microsoft.Extensions.Options;
using Azure.AI.OpenAI;
using OpenAI.Chat;

namespace DuckCreek.ComandLine.Tool.Clients.Gpt;

public interface IAzureOpenAiClient
{
    Task<string> SummarizeTextAsync(string content);
}

internal sealed class AzureOpenAiClient(IOptions<OpenAiOptions> options) : IAzureOpenAiClient
{
    public async Task<string> SummarizeTextAsync(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return string.Empty;
        }

        var azureClient = new AzureOpenAIClient(new Uri(options.Value.BaseUrl), new ApiKeyCredential(options.Value.ApiKey));
        var chatClient = azureClient.GetChatClient(options.Value.Model);

        ChatCompletion completion = await chatClient.CompleteChatAsync
        (
            [
                new SystemChatMessage
                (
                    "You are a helpful assistant that summarizes long texts into concise, well-structured Markdown with headings and bullet points"
                ),
                new UserChatMessage(content)
            ]
        );

        return completion.Content[0].Text;
    }
}