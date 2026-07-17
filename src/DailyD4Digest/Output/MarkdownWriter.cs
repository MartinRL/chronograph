using System.Text.RegularExpressions;
using DailyD4Digest.Models;
using Microsoft.Extensions.Logging;

namespace DailyD4Digest.Output;

public sealed partial class MarkdownWriter(ILogger<MarkdownWriter> logger)
{
    // ponytail: escape only currency ($ before a digit) so KaTeX doesn't parse
    // "$141 ... $8.23" as inline math. Genuine $x$ math delimiters are left alone.
    [GeneratedRegex(@"(?<!\\)\$(?=\d)")]
    private static partial Regex CurrencyDollar();

    public async Task WriteAsync(DailyBrief brief, string outputDir, CancellationToken ct = default)
    {
        // Briefs are grouped into month subfolders (YYYY-MM) to keep listings short
        var monthDir = Path.Combine(outputDir, $"{brief.Date:yyyy-MM}");
        Directory.CreateDirectory(monthDir);

        var fileName = $"{brief.Date:yyyy-MM-dd}.md";
        var filePath = Path.Combine(monthDir, fileName);

        if (File.Exists(filePath))
        {
            logger.LogInformation("Brief already exists at {Path}, skipping", filePath);
            return;
        }

        // The synthesis prompt should produce the full markdown including frontmatter.
        // If the model didn't include frontmatter, prepend it.
        var content = CurrencyDollar().Replace(brief.Markdown, @"\$");
        if (!content.StartsWith("---"))
        {
            var frontmatter = $"""
                ---
                tags:
                  - daily-D4-digest
                  - agentic-engineering
                  - ai-research
                date: {brief.Date:yyyy-MM-dd}
                sources_scanned: {brief.SourcesScanned}
                items_scored: {brief.ItemsScored}
                items_selected: {brief.ItemsSelected}
                ---

                """;
            content = frontmatter + content;
        }

        await File.WriteAllTextAsync(filePath, content, ct);
        logger.LogInformation("Wrote daily brief to {Path}", filePath);
    }
}
