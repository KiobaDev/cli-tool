namespace DuckCreek.ComandLine.Tool.Clients.HtmlFetching;

public interface IHtmlFetcherClient
{
    Task<string> FetchHtmlAsync(string url, CancellationToken cancellationToken = default);
}

internal sealed class HtmlFetcherClient(HttpClient httpClient) : IHtmlFetcherClient
{
    public async Task<string> FetchHtmlAsync(string url, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}