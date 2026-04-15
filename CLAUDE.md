# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Chronograph is a deployment of **Quartz v4** — a TypeScript-based static site generator for publishing markdown content as a website. It transforms markdown files (in `content/`) into a static site deployed to GitHub Pages at `martinrl.github.io/chronograph`.

It hosts two things:
1. **Long-form essays** — the Software Civil Engineering thesis (`content/software-civil-engineering.md`)
2. **Daily D4 Digest** — a .NET 10 pipeline that generates daily intelligence briefs on agentic engineering (`content/digest/briefs/`)

## Common Commands

```bash
npm run check          # Type-check (tsc --noEmit) + Prettier format check
npm run format         # Auto-format with Prettier
npm test               # Run tests (tsx --test, Node native test runner)
npx quartz build       # Build the site
npx quartz build --serve  # Build and serve locally with hot reload
npx quartz build --bundleInfo -d docs  # Build with bundle analysis (used in CI)
```

## Architecture

### Plugin Pipeline

The core build processes markdown through a three-stage plugin pipeline defined in `quartz.config.ts`:

1. **Transformers** (`quartz/plugins/transformers/`) — Parse and modify markdown content (frontmatter extraction, syntax highlighting, LaTeX, GFM, link crawling, table of contents)
2. **Filters** (`quartz/plugins/filters/`) — Remove content from the build (e.g., drafts)
3. **Emitters** (`quartz/plugins/emitters/`) — Generate output files (HTML pages, RSS, sitemap, OG images, assets)

Plugin interfaces are defined in `quartz/plugins/types.ts`.

### Build System

- **Entry point**: `quartz/bootstrap-cli.mjs` → `quartz/build.ts`
- **Bundler**: esbuild with esbuild-sass-plugin
- **Markdown pipeline**: unified/remark/rehype
- **Frontend**: Preact (JSX configured with `react-jsx` pragma pointing to `preact`)

### Key Configuration Files

- `quartz.config.ts` — Site configuration: plugins, theme colors, typography, locale
- `quartz.layout.ts` — Page layout: which components appear in header, sidebars, footer

### Components (`quartz/components/`)

Preact components used for page rendering. Each component typically exports a Quartz component (not a raw Preact component) via the component factory pattern. Components can include associated SCSS files in `quartz/styles/`.

### Content

Markdown files live in `content/`. The build ignores patterns listed in `quartz.config.ts` (`private`, `templates`, `.obsidian`, `src`, `*.csproj`, `*.slnx`, `*.cs`, `*.json`).

## Code Style

- **Formatter**: Prettier — no semicolons, trailing commas, 2-space indent, 100 char print width
- **TypeScript**: Strict mode enabled, ESNext target, unused variables/params are errors
- **Commits**: Conventional commit format (`feat:`, `fix:`, `docs:`, `chore:`, etc.)
- **Node requirement**: >= 22
- **Module system**: ES modules (`"type": "module"`)

## CI

GitHub Actions (`.github/workflows/ci.yaml`) runs on push/PR to `v4`:
- Matrix: Windows, macOS, Ubuntu
- Steps: `npm ci` → `npm run check` → `npm test` → build

Deployment (`.github/workflows/deploy.yml`) pushes to GitHub Pages on `v4` branch pushes.

Daily brief generation (`.github/workflows/daily-brief.yml`) runs at 05:00 UTC daily — generates a brief via the .NET pipeline, commits to `v4`, which then triggers the deploy workflow.

## Daily D4 Digest Pipeline

A C# .NET 10 console app that collects, scores, and synthesizes a daily intelligence brief.

### Pipeline Architecture

```
Sources (RSS, arXiv, Reddit, Bluesky)
  → Collect → Dedup → Score (Sonnet) → Enrich → Synthesize (Opus) → Write
  → content/digest/briefs/YYYY-MM-DD.md
  → Quartz → GitHub Pages
```

### Key Directories

| Path | Purpose |
|------|---------|
| `src/DailyD4Digest/` | .NET console app — the pipeline |
| `src/DailyD4Digest/Config/` | feeds.json, dimensions.json, prompts/ |
| `content/digest/briefs/` | Generated daily markdown briefs |
| `content/digest/` | D4 Digest content root |

### Data Sources

All free, no API keys (except Anthropic for AI):
- **RSS feeds** — blogs (Willison, Fowler, Latent Space), HN filtered, InfoQ, arXiv daily
- **arXiv API** — search by category + keywords (cs.SE, cs.AI, cs.MA)
- **Reddit JSON** — top posts from relevant subreddits (no auth needed)
- **Bluesky API** — public search (no auth needed)

### Models

- **Sonnet 4.6** — scoring (structured JSON, fast, batch processing)
- **Opus 4.6** — synthesis (deeper analysis, SCE cross-cutting lens)

### The D1-D4 Framework

| | Internal | External |
|---|---|---|
| Building | **D1: Agentic Engineering** | **D2: AI in the Product** |
| Scaling | **D4: Performance & Cost** | **D3: Build for Agents** |

### Running the Pipeline

```bash
export ANTHROPIC_API_KEY=sk-ant-...
dotnet run --project src/DailyD4Digest
```

Output: `content/digest/briefs/YYYY-MM-DD.md`

### Pipeline Conventions

- Config changes go in `feeds.json` or `dimensions.json`, not in code
- Prompts live in `Config/prompts/` as `.md` files — edit freely
- `.slnx` solution format (not `.sln`)
- Output is Obsidian-flavored Markdown (frontmatter, callouts, wikilinks)
