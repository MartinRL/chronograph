---
title: "Martin Rosén-Lidholm, Cases"
description: "Selected cases evidencing the claims in the CV: AI-native transformation, product operating models, and strategy-to-architecture alignment."
created: 2026-08-03
tags: [cases]
---
# Martin Rosén-Lidholm — Cases

+45 31 76 37 01 | martin@rosenlidholm.se | [linkedin.com/in/martin-rosen-lidholm](https://www.linkedin.com/in/martin-rosen-lidholm/)

This document is a portfolio of selected cases that evidence the claims in [my CV](https://martinrl.github.io/cv): making organizations AI-native, installing product operating models, and aligning strategy, architecture, and org design. Each case follows Situation / Complication / Resolution and closes with the transferable pattern.

## Agentic / AI-native

### Rebuilding an Organization for Agentic Product Engineering (ChronosHub, 2026-)

**Situation:** A scholarly publishing SaaS company (owned by ACS, largest customer IEEE) with a 30-person Product & Engineering organization built for a pre-agentic world: conventional SDLC, human-only consumption assumptions, and a Vue SPA-era stack.

**Complication:** Agentic AI changes all three layers at once: how software is built, what the product is, and who (or what) consumes it. Incremental adoption of coding assistants would not capture the shift, and an unmanaged transformation would break delivery and governance simultaneously.

**Resolution:** Rebuilding the organization from the ground up around my D4 strategy lens: D1 Agentic Engineering (how we build), D2 AI in the Product (what we build), D3 Build for Agents (who consumes), D4 Performance & Cost (how we sustain). Maturity is assessed and staged against the Agentic Coding Maturity Model. As executive sponsor of FrontierHub, extending the transformation beyond Product & Engineering to move the whole company toward an AI frontier firm. Inherited a platform where no marketable feature had shipped in 15 months; six months in, a two-week release cadence holds (defect share 63% to 25%), incident detection dropped from weeks to minutes, and the Prototype Factory turns event-model specs into working prototypes, with the Product Factory underway.

**Transferable pattern:** Treat agentic AI as an organizational redesign with an explicit strategy lens and maturity model, not a tooling rollout.

## Management / Leadership

### Team Flow Efficiency (SaaS scale-up, ~10 teams, ~80 engineers, 5 sites)

**Situation:** Ten teams had worked from a well-defined feature-parity backlog for two years, supported by Scrum, through a monolith-to-self-contained-systems transformation. The company was shifting from feature teams to outcome-measured product teams.

**Complication:** Boards and ceremonies optimized for feature delivery turned against the teams once substantial upstream discovery was added; Scrum's time constraints conflicted with discovery work managed on the same delivery boards.

**Resolution:** Coached all Engineering Managers (including outside my direct reports) on kanban and lean principles: flow efficiency, work visualization, fast feedback loops. Separated discovery and delivery into distinct boards with columns reflecting real process steps, and managed work-item aging rather than arbitrary time-boxes, enabling real-time feedback between discovery and delivery.

**Transferable pattern:** When the operating model changes, change the work system with it; aging work items are a more tangible lever than time-boxes. Outcomes (changed user behaviors and problems solved) matters more than output (features/product increments).

### From Project to Product Organization (bank/pension software, ~30 developers, gazelle growth)

**Situation:** Revenue split evenly between time-and-materials and recurring licenses. Project Managers were evaluated on timely delivery; resource allocation ran through a monthly spreadsheet controlled by the PMO.

**Complication:** Some contracts hurt the bottom line, engagement and code health metrics were declining, and allocation regularly exceeded 100% of capacity while the model diverged from reality.

**Resolution:** Restructured around product teams, each led by a trio (Project Manager, Domain Architect, Business Analyst) with shared outcome accountability. Trained teams in flow efficiency and kanban and let them design their own processes. Established a CEO-led Flow Office (influenced by Actionable Metrics at Siemens Health Services) measuring flow efficiency, facilitating reprioritization, and driving kaizen; paired each trio role with an executive counterpart for alignment.

**Transferable pattern:** Project-to-product is an incentive and accountability redesign; process training alone does not move it.

### Inverse Conway Maneuver (SaaS scale-up, 3 European sites)

**Situation:** An unexpected economic downturn forced a shift from scaling up to scaling down, under constraints of colocated teams per site and centralized system ownership.

**Complication:** The current team topology, self-contained systems landscape, and ownership model made fast downscaling impossible.

**Resolution:** Executed an Inverse Conway Maneuver: analyzed which systems to merge and how to realign team structures to value streams and compliance, then implemented the new organizational and architectural structure, maintaining operational efficiency at reduced scale.

**Transferable pattern:** Downscaling is an architecture problem as much as an org problem; redesign both together or neither works.

### Career Ladder (SaaS company, ~60 engineers, 10 EMs, 4 sites across Europe)

**Situation:** No explicit career ladder existed across a distributed engineering organization.

**Complication:** Missing promotion policies and force-multiplier (Staff+) roles left cross-team problems unowned and created confusion and dissatisfaction about progression.

**Resolution:** The Engineering management team collaboratively built a well-defined ladder with clear promotion and feedback processes. Within three months: two Engineers promoted to Staff, two Engineering Managers to senior roles, plus transparent communication of "why not" decisions.

**Transferable pattern:** A career ladder is as much about creating force-multiplier roles that own cross-team problems as it is about fairness.

## Business / Strategy

### Software Development Strategy via Wardley Mapping (project-based org, rising compliance demands)

**Situation:** No cohesive software development strategy: no settled positions on tech stack, cloud adoption, or buy-vs-build. Limited software expertise in the executive team; project silos driven by commercial goals.

**Complication:** Individual projects could not align to a long-term strategy, making it hard to address core/supporting/generic domains, execute a cloud-native transformation, or form holistic SaaS partnerships.

**Resolution:** Co-organized and ran a full-day Wardley Mapping workshop for the entire management team plus selected leaders and specialists, producing a shared map of current position and strategic direction that aligned the organization around a unified software development strategy.

**Transferable pattern:** A shared map beats a shared document; executives align on positions and movements, not bullet lists.

### Minimum Viable Telco Internal Startup (Telenor DK)

**Situation:** A red-ocean mobile subscription market with intense rivalry and diminishing differentiation.

**Complication:** Declining revenue against high operational costs: a race to the bottom where price cuts were not matched by cost efficiencies.

**Resolution:** A team of four seniors (myself as Chief Software Architect, a former CEO, a Business Analyst, a Solution Architect) used Wardley Mapping to design a model reducing operational costs by ~90%: Internet-only, a single offering, commodity components, effectively applying Musk's algorithm to achieve extreme cost reduction.

**Transferable pattern:** Map-driven simplification can find order-of-magnitude cost structures that incremental optimization never will.

## Architecture / Development

### MVNO Business Support System (Telenor, multi-year)

**Situation:** A BSS designed for many small wholesale MVNO customers: single-tenant clusters of ~100 services sharing one database, some even sharing schema. Strategy shifted to supporting a single large internal MVNO.

**Complication:** An architecture optimized for many small customers directly opposed the need to rapidly expand capabilities, integrations, and innovation for one large customer, straining delivery and agility.

**Resolution:** Re-architected for speed of feature delivery: a modular monolith with event modeling, tactical DDD, event sourcing, and CQRS; migrated from SQL Server to Marten/PostgreSQL as part of the cloud strategy, realigning the architecture with the business objectives.

**Transferable pattern:** Architectural fitness is strategy-relative; a "good" architecture becomes the bottleneck the day the strategy changes.

### Accrued Bond Interest (financial software)

**Situation:** The sole engineer behind a critical C++ system for calculating accrued bond interest, ten years of single-contributor code, was leaving.

**Complication:** No remaining C++ expertise, no tests: any modification was high-risk.

**Resolution:** With another Senior Engineer, rewrote the system in .NET, driven by stakeholders and subject-matter experts through spreadsheet examples placed under source control as specifications by example. The process surfaced several severe pre-existing bugs and produced a robust, maintainable implementation.

**Transferable pattern:** When domain knowledge lives in experts' heads and spreadsheets, make the spreadsheets the executable specification.

### Visualize System Behavior (telco BSS)

**Situation:** A complex business support system that was difficult to understand, develop, and enhance.

**Complication:** Low throughput, slow incident response, and a strained engineering team.

**Resolution:** Modeled system behavior end-to-end via event modeling of both traditional systems and parts under development, including planned features; the model outgrew Miro and migrated to Excel for the extensive CRUD visualization.

**Transferable pattern:** Making system behavior visible is the cheapest capacity increase available to a strained team.

## Cycling

### Race Around Poland — 3,600 km / 33,000 hm Unsupported

**Situation:** After brevets and ultra-races of 1,600-2,000 km, I targeted one of the truly long races. The pandemic cancelled my North Cape 4000 plans, so I entered RAP 2022 to avoid border crossings.

**Complication:** My aero racer, optimized for shorter ultras, was ill-suited to the distance and Poland's rough roads. Around 300 km in, a crash followed by an unrepairable mechanical forced me to scratch.

**Resolution:** I spent the family vacation dot-watching the race and designing the right bike on paper, built it over the winter, and returned for RAP 2023 with a sub-10-day, top-10 goal. I finished in 259:45, 11th solo unsupported, just shy of both targets, on a bike that performed exactly as designed.

**Transferable pattern:** After a failure, redesign the system, not the effort; then re-enter with pre-committed goals.
