using System.Text.Json;
using Anthropic;
using Anthropic.Core;
using Anthropic.Models.Messages;
using DailyD4Digest.Models;
using Microsoft.Extensions.Logging;

namespace DailyD4Digest.Synthesis;

public sealed class BriefSynthesizer(ILogger<BriefSynthesizer> logger)
{
    public async Task<DailyBrief> SynthesizeAsync(
        IReadOnlyList<ScoredItem> items,
        int totalScanned,
        int totalScored,
        CancellationToken ct = default)
    {
        _ = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY")
            ?? throw new InvalidOperationException("ANTHROPIC_API_KEY not set");

        var httpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };
        var client = new AnthropicClient(new ClientOptions
        {
            HttpClient = httpClient,
            Timeout = TimeSpan.FromMinutes(5),
            MaxRetries = 4
        });

        var promptPath = Path.Combine(AppContext.BaseDirectory, "Config", "prompts", "synthesis.md");
        var systemPrompt = await File.ReadAllTextAsync(promptPath, ct);

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var itemsJson = JsonSerializer.Serialize(items.Select(i => new
        {
            i.Item.Title,
            i.Item.Url,
            i.Item.Summary,
            i.Item.Source,
            i.Item.Author,
            i.D1Score,
            i.D2Score,
            i.D3Score,
            i.D4Score,
            i.SceScore,
            EnrichedContent = i.EnrichedContent.Length > 0
                ? i.EnrichedContent[..Math.Min(i.EnrichedContent.Length, 2000)]
                : ""
        }), new JsonSerializerOptions { WriteIndented = true });

        var userMessage = $"""
            Today is {today:yyyy-MM-dd}.
            Stats: {totalScanned} sources scanned, {totalScored} items scored, {items.Count} selected.

            Generate the daily D4 digest from these scored and enriched items:

            {itemsJson}
            """;

        logger.LogInformation("Synthesizing brief with {Count} items via Opus", items.Count);

        try
        {
            var response = await client.Messages.Create(new MessageCreateParams
            {
                Model = "claude-opus-4-6",
                MaxTokens = 8192,
                System = systemPrompt,
                Messages = [new()
                {
                    Role = Role.User,
                    Content = userMessage,
                }]
            }, ct);

            var markdown = string.Join("", response.Content
                .Select(block => block.TryPickText(out var text) ? text.Text : ""));

            markdown = StripCodeFences(markdown);

            return new DailyBrief
            {
                Date = today,
                Markdown = markdown,
                SourcesScanned = totalScanned,
                ItemsScored = totalScored,
                ItemsSelected = items.Count
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Synthesis failed after retries");
            throw;
        }
    }

    private static string StripCodeFences(string text)
    {
        // ponytail: model sometimes emits frontmatter, then repeats the whole doc inside a fence.
        // Keep the fenced copy (it's the complete document). Trim first (leading whitespace broke
        // the \A anchor on 2026-08-16) and loop in case the pattern nests.
        text = text.Trim();
        while (System.Text.RegularExpressions.Regex.Match(
            text, @"\A---\r?\n[\s\S]*?\r?\n---\s*\r?\n```(?:markdown|md)?\s*\r?\n(---[\s\S]*)\z") is { Success: true } doubled)
            text = doubled.Groups[1].Value;

        if (text.StartsWith("```markdown", StringComparison.OrdinalIgnoreCase))
            text = text["```markdown".Length..];
        else if (text.StartsWith("```md", StringComparison.OrdinalIgnoreCase))
            text = text["```md".Length..];
        else if (text.StartsWith("```"))
            text = text[3..];

        if (text.EndsWith("```"))
            text = text[..^3];

        return text.Trim();
    }
}
