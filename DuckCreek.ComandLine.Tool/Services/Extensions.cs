using Microsoft.Extensions.DependencyInjection;

namespace DuckCreek.ComandLine.Tool.Services;

public static class Extensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        return services.AddScoped<IDocumentSummaryService, DocumentSummaryService>();
    }
}