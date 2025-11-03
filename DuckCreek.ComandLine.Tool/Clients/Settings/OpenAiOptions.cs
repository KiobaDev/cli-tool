namespace DuckCreek.ComandLine.Tool.Clients.Settings;

internal sealed class OpenAiOptions
{
    public const string SectionName = "OpenAi";

    public required string BaseUrl { get; init; }
    public required string ApiKey { get; init; }
    public required string Model { get; init; }
}