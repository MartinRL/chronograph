# Chronograph

A Quartz-powered publication site for long-form essays and daily intelligence briefs on software engineering, agentic AI, and the professionalization of the discipline.

Published at [martinrl.github.io/chronograph](https://martinrl.github.io/chronograph).

## Content

### Articles

- [Software Civil Engineering: From Craft to Discipline](https://martinrl.github.io/chronograph/software-civil-engineering) — Why agentic AI demands the professionalization of software production

### Daily D4 Digest

A daily intelligence brief on agentic engineering, analyzed through the D1-D4 framework and the Software Civil Engineering thesis.

|             | Internal                    | External                   |
| ----------- | --------------------------- | -------------------------- |
| Building    | **D1: Agentic Engineering** | **D2: AI in the Product**  |
| Scaling     | **D4: Performance & Cost**  | **D3: Build for Agents**   |

Briefs are generated automatically at 05:00 UTC by a .NET 10 pipeline that collects from RSS, arXiv, Reddit, and Bluesky, scores relevance with Sonnet, and synthesizes with Opus.

Browse briefs at [martinrl.github.io/chronograph/digest](https://martinrl.github.io/chronograph/digest).

## Architecture

```
Sources (RSS, arXiv, Reddit, Bluesky)
  -> Collect -> Dedup -> Score (Sonnet) -> Enrich -> Synthesize (Opus) -> Write
  -> content/digest/briefs/YYYY-MM-DD.md
  -> Quartz -> GitHub Pages
```

| Path | Purpose |
|------|---------|
| `content/` | Quartz content root (essays, infographics) |
| `content/digest/briefs/` | Generated daily briefs |
| `src/DailyD4Digest/` | .NET 10 console app — the pipeline |
| `src/DailyD4Digest/Config/` | feeds.json, dimensions.json, prompts/ |
| `quartz/` | Quartz v4 static site generator |

## Running locally

```bash
# Build the site
npx quartz build --serve

# Run the digest pipeline
export ANTHROPIC_API_KEY=sk-ant-...
dotnet run --project src/DailyD4Digest
```
