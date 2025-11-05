using DuckCreek.ComandLine.Tool.Prompts.Enums;
using DuckCreek.ComandLine.Tool.Prompts.Strategies.Interfaces;

namespace DuckCreek.ComandLine.Tool.Prompts;

public interface IPromptProvider
{
    string Provide(PromptType promptType);
}

internal sealed class PromptProvider(IEnumerable<IPromptStrategy> strategies) : IPromptProvider
{
    public string Provide(PromptType promptType)
    {
        var strategy =  strategies.FirstOrDefault(x => x.PromptType == promptType);

        if (strategy is null)
        {
            throw new ArgumentException($"No strategy found for prompt type: {promptType}", nameof(promptType));
        }

        return strategy.GetPrompt();
    }
}