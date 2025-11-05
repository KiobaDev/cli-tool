using DuckCreek.ComandLine.Tool.Prompts.Strategies.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DuckCreek.ComandLine.Tool.Prompts.Strategies;

internal static class Extensions
{
    public static IServiceCollection RegisterPromptStrategies(this IServiceCollection services)
    {
        return services.AddScoped<IPromptProvider, PromptProvider>()
                       .AddSingleton<IPromptStrategy, SummarizationPromptStrategy>();
    }
}