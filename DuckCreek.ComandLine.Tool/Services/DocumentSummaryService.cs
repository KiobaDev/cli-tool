using DuckCreek.ComandLine.Tool.Clients.Gpt;
using DuckCreek.ComandLine.Tool.Clients.HtmlFetching;
using DuckCreek.ComandLine.Tool.Prompts.Enums;
using DuckCreek.ComandLine.Tool.Prompts.Strategies.Interfaces;

namespace DuckCreek.ComandLine.Tool.Services;

public interface IDocumentSummaryService
{
    Task GenerateMarkdownSummaryAsync(string url, string outputPath, CancellationToken ct = default);
}

internal sealed class DocumentSummaryService
(
    IHtmlFetcherClient htmlFetcherClient, 
    IAzureOpenAiClient azureOpenAiClient,
    IEnumerable<IPromptStrategy> promptStrategies
) : IDocumentSummaryService
{
    public async Task GenerateMarkdownSummaryAsync(string url, string outputPath, CancellationToken ct = default)
    {
        var promptStrategy = promptStrategies.First(s => s.PromptType is PromptType.SummarizationPrompt);
        
        var websiteContent = await htmlFetcherClient.FetchHtmlAsync(url, ct);
        var summary = await azureOpenAiClient.SummarizeTextAsync(websiteContent, promptStrategy.GetPrompt());
        
        await SaveMarkdownFileAsync(url, summary, outputPath, ct);
    }

    private static async Task SaveMarkdownFileAsync(string url, string summary, string outputPath, CancellationToken ct = default)
    {
        var directory = Path.GetDirectoryName(outputPath);
        
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var markdownContent = $"# Summary\n\n**Source**: {url}\n\n{summary}\n";
        
        await File.WriteAllTextAsync(outputPath, markdownContent, ct);
    }
}