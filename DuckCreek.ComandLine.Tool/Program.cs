using Cocona;
using DuckCreek.ComandLine.Tool.Clients;
using DuckCreek.ComandLine.Tool.Prompts.Strategies;
using DuckCreek.ComandLine.Tool.Services;
using Microsoft.Extensions.Configuration;

const string settings = "appsettings.json";

var builder = CoconaApp.CreateBuilder(args);

builder.Configuration.AddJsonFile(settings, optional: false, reloadOnChange: true);

builder.Services.RegisterHttpClients(builder.Configuration);
builder.Services.RegisterServices();
builder.Services.RegisterPromptStrategies();

var app = builder.Build();

app.AddCommand("summarize", async 
(
    IDocumentSummaryService documentSummaryService,
    [Option] string url,
    [Option] string output
) =>
{
    await documentSummaryService.GenerateMarkdownSummaryAsync(url, output);
}).WithDescription("Summarize a long-form article from URL and save to Markdown");

await app.RunAsync();
