namespace DuckCreek.ComandLine.Tool.Services;

public interface IHtmlFetcher
{
    Task<string> FetchHtmlAsync(string url, CancellationToken cancellationToken = default);
}

internal sealed class HtmlFetcher(HttpClient httpClient) : IHtmlFetcher
{
    public async Task<string> FetchHtmlAsync(string url, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}