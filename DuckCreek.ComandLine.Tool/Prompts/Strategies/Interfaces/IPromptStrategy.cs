using DuckCreek.ComandLine.Tool.Prompts.Enums;

namespace DuckCreek.ComandLine.Tool.Prompts.Strategies.Interfaces;

internal interface IPromptStrategy
{
    PromptType PromptType { get; }
    
    string GetPrompt();
}

