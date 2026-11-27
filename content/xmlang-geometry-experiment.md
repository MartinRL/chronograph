---
title: "Challenging the geometry prohibition: a pre-registered experiment"
description: "Can a minimal closed-set region vocabulary (slot: header|body|footer) buy cross-transformer determinism without starting the MBUI slide? Hypotheses, rubric, probe prompt, tripwires, and decision rule — fixed before any evidence is collected."
created: 2026-08-23
tags: [xmlang, experiment, pre-registration, experience-modeling, geometry]
---
# Challenging the geometry prohibition: a pre-registered experiment

**This document is immutable after its first commit.** Results are published separately (research article §10). That is what makes "pre-registered" honest: the hypotheses, the measurement instrument, the probe prompt, the tripwires, and the decision rule are all fixed here, before the first data point exists.

## Background

xmlang v0.2's core dogma is the geometry prohibition: *"an Experience Model records judgment as data, never as geometry… this specification defines no geometric vocabulary and future versions MUST NOT add one"* ([spec](xmlang-spec.md)). The research article admits Risk #1: the rule is a discipline, not a mechanism, and the slope from salience tiers to layout syntax is short. The §9 field test covered only 8 surfaces.

This experiment deliberately challenges the prohibition on its strongest available ground. A clean-room prototype of BlindBudet, built from the spec pair alone with zero geometry in the model, already converged on the live layout. So geometry cannot buy correctness here; the only thing left to buy is **cross-transformer determinism** — turning "an agent will probably put commands at the bottom" into "the model says so". The experiment measures whether that purchase is real, worth its vocabulary cost, and achievable without starting the MBUI slide.

## The candidate vocabulary

One optional key per composition item: `slot:`, a closed enum `header | body | footer`. Absent = transformer judgment per the Defaults table. Named regions, not coordinates: no dimensions, no breakpoints, no relative-position pairs, no per-field regions. The concrete renderer's type heuristics stay concrete-stratum.

A known expressiveness gap, on purpose: today's reference renderer splits ONE view's fields across TWO regions (secondary text fields become header pills; other secondary fields become body cards). A per-item `slot:` cannot express that. If per-FIELD regions start feeling necessary during the experiment, that is tripwire (b) firing — logged as evidence, never solved.

## Hypotheses

- **H1**: a minimal closed-set region vocabulary measurably increases clean-room layout convergence on decisions the un-geometried spec leaves variable, with zero vocabulary growth and zero special-casing across the experiment.
- **H0-dead-weight**: the vocabulary restates what salience tiers + the Defaults table already imply — baseline probes already converge, or every BlindBudet slot annotation equals its default, or probe agents ignore the keys.
- **H0-slope**: the first geometry key demands a second (see tripwires).

Both null outcomes are publishable: "prohibition re-affirmed" is as good a result as "prohibition amended".

## Protocol

1. **Pre-registration** (this document), committed first.
2. **Baseline probe C1**: 3 clean-room agent runs building a playable prototype from the v0.2 spec pair (`specs/blindbudet-event-model.yaml` + `specs/blindbudet.xm.yaml`), never seeing `src/` or the live site. Scored against the rubric. **Early exit**: all 3 runs converge on all 8 items → H0-dead-weight confirmed with n=4 (including the original prototype); publish the re-affirmation, zero code written.
3. **Vocabulary implementation** (only if C1 shows variance): spec amended to v0.3.0-experimental; parser, linter (`xm-unknown-slot` error, `xm-slot-restates-default` info), and reference renderer gain `slot:` support. No-slot rendering must stay byte-identical (the untouched parity suites of two sibling games are the instrument).
4. **Annotation hunt**: walk all 8 BlindBudet surfaces for placements the live design genuinely wants that differ from defaults. Every annotation must be a real judgment, not a syntax exercise. Zero genuine non-defaults found = falsification, logged. The `xm-slot-restates-default` info count is the dead-weight metric.
5. **Treatment probe C2**: 3 clean-room runs from the v0.3 spec pair, same prompt verbatim.
6. **Verdict** per the decision rule; keep or revert; publish raw scores and the tripwire log.

The probe prompt names the xmlang specification file as an allowed input. Because the spec file itself is amended between C1 and C2, the prompt stays verbatim-identical across conditions while the spec version varies with the condition — which is the treatment.

## Probe prompt (verbatim, both conditions)

```
You are building a clean-room prototype. Your ONLY permitted inputs are these
three files:

- specs/blindbudet-event-model.yaml   (emlang Event Model)
- specs/blindbudet.xm.yaml            (xmlang Experience Model)
- C:\code\GitHub\MartinRL.github.io\articles\xmlang-spec.md   (the xmlang specification)

Read all three, then build ONE single self-contained HTML file (inline CSS and
JS, no external resources) that is a playable prototype of the game the spec
pair describes: a JavaScript decider implementing the Event Model's commands,
events, and GWT semantics, and screens rendered per the Experience Model
(personas, surfaces, salience tiers, prominence, labels, tokens). Show each
persona's experience side by side as phone-sized frames. Embed the Event
Model's GWT test cases as executable checks with a visible pass/fail badge.

Where the spec pair does not determine a presentation decision, use your own
judgment as a transformer conforming to the xmlang specification (including
its Defaults section).

Hard constraints:
- Do NOT read any other file in either repository: no src/, no prototypes/,
  no wwwroot/, no docs/, no other specs, no articles besides the xmlang spec.
  Do not fetch any URL.
- Output exactly one file: {OUT}. Write it and stop.
- Work autonomously; do not ask questions.
```

`{OUT}` is the only substitution, a per-run output path.

## Determinism rubric

Each probe run is scored 0/1 per item against the live design's choices:

1. Commands below all view content on every surface
2. Secondary scalar fields as compact chrome near the heading (pills), not full-width blocks
3. On-demand fields behind a disclosure, closed by default
4. Primary fields visually dominant and first
5. Single-column stack per persona frame
6. Surface label renders first (heading)
7. LobbyVärd: join affordance (QR/code) in header above roster
8. Rundresultat: trueWorth prominent above the per-player profit list

Scoring is mechanical from the produced DOM; each scoring sheet quotes the DOM evidence per item. Known limitation, stated up front: the scorer is not blind to condition (same person implements and scores). Mitigation: the items are binary and structural, and the quoted evidence makes every score auditable.

## Slope tripwires

Any single fire falsifies H1, regardless of rubric deltas:

- **(a)** a per-surface or per-game special case needed in the reference renderer
- **(b)** a second geometry key (including a per-field region axis) needed or seriously proposed — during annotation too
- **(c)** thought-experiment re-expression of the two sibling games demands values outside `header | body | footer`
- **(d)** any annotation reorders a `compose` list to achieve position rather than importance (checkable: the annotation diff must not reorder)
- **(e)** any new lint helper needs the Event Model as a parameter (slot legality must be decidable from the Experience Model alone)

An implementation diary logs every resisted temptation for more vocabulary as slope data.

## Decision rule (tripwires dominate)

| Outcome | Condition | Action |
|---|---|---|
| Re-affirm (early) | C1 3/3 on all 8 items | Stop before implementation; publish |
| Re-affirm (dead weight) | Zero genuine non-default slots OR C2 delta ≈ 0 OR agents ignore the keys | Revert spec (v0.4.0 changelog entry) + implementation commits |
| Re-affirm (slope) | Any tripwire fires | Same revert; the tripwire log is the headline |
| **Promote v0.3.0** | C1 varied on ≥1 item AND C2 3/3 on those items AND ≥1 genuine non-default slot AND zero tripwires | Keep; drop `-experimental` |
| Mixed (delta + tripwire) | Determinism gained but a tripwire fired | Revert — determinism gains do not buy back the slope |

## What would NOT count

- Convergence on items the v0.2 pair already determines (that is the baseline doing its job)
- Prettier prototypes, better copy, stronger test badges — only the 8 rubric items score
- A "keep" verdict justified by future hypothetical needs — only observed C1 variance closed by observed C2 convergence counts
