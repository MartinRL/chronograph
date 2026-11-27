---
title: "Geometry experiment: probe scoring sheets"
description: "Raw rubric scores, annotation-hunt log, and tripwire log for the pre-registered xmlang geometry-prohibition experiment. C1 = baseline (v0.2 spec pair); the experiment falsified at Phase 4, C2 never ran. Verdict: prohibition re-affirmed."
created: 2026-08-23
tags: [xmlang, experiment, data]
---
# Probe scoring sheets

Protocol and rubric: [pre-registration](xmlang-geometry-experiment.md). Probe outputs: `kvissig.se/prototypes/geometry-probe/`. Each item scored 0/1 against the live design; evidence is quoted from the produced DOM/render code.

## C1 — baseline (v0.2 spec pair), 2026-08-23

| # | Rubric item | run1 | run2 | run3 |
|---|---|---|---|---|
| 1 | Commands below all view content on every surface | 0 | 0 | 0 |
| 2 | Secondary scalars as pills near the heading | 0 | 0 | 0 |
| 3 | On-demand behind disclosure, closed by default | 1 | 1 | 1 |
| 4 | Primary fields dominant and first | 1 | 1 | 1 |
| 5 | Single-column stack per persona frame | 1 | 1 | 1 |
| 6 | Surface label renders first | 1 | 1 | 1 |
| 7 | LobbyVärd: join affordance above roster | 1 | 1 | 1 |
| 8 | Rundresultat: trueWorth above profit list | 1 | 1 | 1 |
| | **Total** | **6/8** | **6/8** | **6/8** |

### Evidence, item 1 (0/3)

All three runs place the on-demand `<details>` (view content) BELOW the command buttons on at least one surface; the live design renders on-demand before commands.

- run1 `renderBudgivning`: `<div class="actions">…PlaceBid…</div>` then `${odBlock(...)}`; same in `renderRundresultat`. Internally inconsistent: `renderLobbyVard` has `odBlock` BEFORE `.actions`.
- run2 `surfLobbyVard`/`surfBudgivning`/`surfRundresultat`: `<button class="cta">` then `<details class="ondemand">`.
- run3 `LobbyVärd`/`Budgivning`/`rundresultat`: `btn-primary` then `${onDemand(...)}`.

### Evidence, item 2 (0/3)

Live: secondary scalar fields render as compact pills in the header area near the heading.

- run1: secondary = `.sec` muted text lines inside body cards below primary content ("Svara i … · Budrunda 1 av 2").
- run2: secondary = `.secondary-txt` centered muted lines placed mid/below content.
- run3: secondary = `.chip` pills (form matches live) but placed at the BOTTOM of/after body cards, not near the heading.

The three runs also disagree with each other on form (text lines vs chips): genuine transformer variance, not merely shared divergence from live.

### Items 3–8 (3/3 each)

All runs: `<details>` without `open`; primary tier in display font 1.5–2.4rem first in the body; 330–340px single-column phone frames; `<h2>` surface heading first; LobbyVärd join code (run2/run3 with a QR/dashed-code card) above the roster; Rundresultat trueWorth as the largest number above the per-player table.

### C1 verdict

No early exit: items 1 and 2 are baseline-variable (0/3 against live; inter-run disagreement on item 2's form, intra-run inconsistency on item 1 in run1). These are precisely region-placement decisions — the treatment (`slot:`) targets them directly. Proceed to Phase 2.

## Phase 4 — the annotation hunt (2026-08-23): falsified

Implementation landed first per protocol (kvissig.se `3cc6913`: parse + `xm-unknown-slot`/`xm-slot-restates-default` lint with an EmSpec-free helper + region rendering with a pinned byte-identical no-slot path; full solution green, parity suites untouched). Then the hunt walked all 8 BlindBudet surfaces for placements the live design genuinely wants that differ from the v0.3 defaults.

**Result: zero genuine non-default slots — provably, not just empirically.** The live BlindBudet UI is rendered BY the default transformer (the xm runtime interpreter, zero per-surface special cases), so no live placement can differ from the defaults. Every candidate annotation:

| Candidate | Verdict |
|---|---|
| `slot: footer` on any of the 5 composed commands (OpenAuction, StartAuction, PlaceBid, AskNextLot, EndAuction) | restates the default — `xm-slot-restates-default` on all 5; dead-weight metric 5/5 |
| `slot: header` for the secondary scalars rendered as header pills (lotIndex/totalLots on Budgivning, pricePaid/lotIndex on Rundresultat) | per-FIELD split (other secondary fields of the same view stay body cards) — below `slot:`'s per-item granularity |
| `slot: header` for LobbyVärd's joinCode QR above the roster | per-FIELD split (joinCode is one field of Roster; the item-level slot would drag the whole roster into the header) |

### Tripwire log

- **(b) FIRES.** The only placements where live judgment does real work are per-field region splits; expressing any of them requires the per-field region axis the pre-registration named as the slope's next step. The first geometry key demands the second to do any work on this testbed. Logged, not solved.
- (a), (c), (d), (e): clean. The lint helper is EmSpec-free; no compose list was reordered (no annotations exist); no per-surface renderer special case; MEM/TTT re-expression needs no values outside `header|body|footer` — it needs no values at all.

### C2 not run

Per protocol step 4, zero genuine non-defaults = falsification. The promote row requires ≥1 genuine non-default slot AND zero tripwires; both are now unsatisfiable, and "What would NOT count" bars a keep justified by anything but observed C2 convergence. Three more probe runs cannot change the action.

## Verdict: prohibition re-affirmed (dead weight + slope)

- **H0-dead-weight confirmed** in its strongest pre-registered form: every possible BlindBudet slot annotation equals its default.
- **H0-slope confirmed**: tripwire (b) fired during annotation.
- **H1 falsified.** The C1 variance (items 1–2) is real, but what would close it is not a geometry key: item 1 is closed by stating the command default ("commands render below all view content" — a Defaults-table row, zero vocabulary), and item 2 is a per-field split `slot:` cannot express by design.

Action per the decision rule: revert — kvissig.se `git revert 3cc6913`, spec to v0.4.0 with a "prohibition re-affirmed" changelog entry. The probe outputs, these scores, and the tripwire log are the published result (research article §10).
