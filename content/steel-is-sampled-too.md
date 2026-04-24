---
title: "Steel Is Sampled, Too"
description: "A Reply to 'No Tensile Strength to Measure'"
created: 2026-04-24
---

# Steel Is Sampled, Too

## A Reply to "No Tensile Strength to Measure"

---

> **Thesis:** The rebuttal's strongest claim, that software cannot be verified because its state spaces are effectively infinite and Rice's theorem forbids decidable verification, reads Rice's theorem as a disabling constraint rather than as what engineers in every mature discipline work around routinely. Civil engineering does not possess what the rebuttal attributes to it. Tensile strength is sampled, not measured on every beam; structural analysis is computed over simplified models, not against reality; "known material properties" are statistical certifications under bounded error. What civil engineering possesses is not physical truth; it is engineering technique under bounded uncertainty. Software has direct analogs: Pex demonstrates that dynamic symbolic execution finds serious defects in industrially-hardened code within a time budget, and Fox and Wei prove that hereditary properties can be tested with query complexity polynomial in 1/ε and independent of input size. The verification analogy holds; it merely does not hold in the strong form the rebuttal imagines it must take.

---

### Executive Summary

- Carsten Jørgensen's rebuttal \[1\] rests on four disanalogies (physics vs. human meaning, requirements volatility, verification asymmetry via Rice's theorem, and a legibility gap), plus Naur's "Programming as Theory Building" \[2\]. Together these are offered as structural reasons the civil engineering analogy fails.
- The load-bearing claim is verification asymmetry. If verification is possible in practice, the other objections shrink from structural barriers to engineering considerations.
- Civil engineering does not verify against physical laws; it verifies against models, bounded by statistical certification of materials and institutional codes. Rice's theorem does not disable software verification any more than the continuum disables steel certification.
- Pex \[3\] is the concrete engineering counterexample: on a core .NET component tested for five years by approximately forty testers, dynamic symbolic execution found seventeen unique errors, including a serious issue, at 43% block coverage, fully automatically.
- Fox & Wei \[4\] are the mathematical counterexample: hereditary permutation properties are two-sided testable with query complexity O(1/ε²), independent of input size. This is the mathematical analog of "measure a few sample beams to certify a batch of steel."
- Naur's theory-building argument over-claims. The strong form is falsified by every codebase that outlives its original authors; the weak form is already conceded in Section 10.4 of the original article (the emergent complexity boundary) \[5\].
- The disanalogy is not between civil engineering and software; it is between *decidable* verification (which neither discipline possesses) and *bounded-confidence* verification (which both disciplines possess).

---

## 1. What the Rebuttal Claims

Jørgensen responds to "Software Civil Engineering" \[5\] by arguing that four structural disanalogies break the comparison between civil engineering and agentic software development:

1. **Physics vs. human meaning.** Civil engineering operates within fixed physical laws with measurable properties; software models human intent and organizational process, which lack comparable stability.
2. **Requirements volatility.** Bridge specifications remain stable after construction begins; software requirements change continuously.
3. **Verification asymmetry.** Civil engineers can fully verify designs against known physical laws; software verification faces fundamental limits (Rice's theorem) because state spaces are "effectively infinite."
4. **Legibility gap.** Formal notations have historically failed to bridge business intent and engineering implementation.

To these Jørgensen adds Peter Naur's "Programming as Theory Building" \[2\]: that programming's essence is the construction of a mental theory residing in human minds, which cannot be fully externalized. Autonomous agents producing code without such theory, on this view, create "programs born dead."

The claims are arranged in ascending order of force. The first is observational; the second and fourth are empirical; the third is presented as a theorem. Only the third, if correct in the strong form offered, is structurally disabling. The argument of this reply is that the third claim misreads Rice's theorem, that two concrete bodies of work falsify the disabling reading, and that the remaining claims shrink to engineering considerations once the third is removed.

## 2. The Central Move: Civil Engineering Does Not Possess What the Rebuttal Grants It

The rebuttal's title, "No Tensile Strength to Measure," presumes civil engineering possesses something software lacks: a direct physical measurement that grounds verification. This presumption is wrong at the level of engineering practice.

Tensile strength is not measured on every beam. It is measured on statistical samples of a batch, certified under a standard (EN 10025, ASTM A36, JIS G3101), and the certification travels with the batch. The individual beam carries a *presumption of conformance*, not a measurement. When sampling rates are inadequate, or a mill misreports, the certification is false, and structures fail. Civil engineering has absorbed several such failures into its codes (the Hyatt Regency walkway collapse, the Silver Bridge) precisely because the discipline understands that its verification is statistical, not direct.

Structural analysis is not performed against physical reality. It is performed against a model: linear elasticity, homogeneous materials, idealized boundary conditions, small deformations. These assumptions are false in the strict sense. The model predicts behavior accurately within a bounded regime; outside that regime, engineering judgment intervenes, or the structure is instrumented and monitored.

Load paths in a building are not fully verified against physical laws. They are verified against the building code, which encodes decades of accumulated failures and successes as enforceable rules. The code is an institutional artifact, not a physical one. It evolves.

What civil engineering possesses is not physical truth. It is **engineering technique under bounded uncertainty**, backed by statistical sampling, approximate models, and institutional codes. Once this is seen, the rebuttal's title inverts. The question is not whether software has tensile strength to measure. It is whether software has the engineering technique, the sampling method, and the institutional code. The answer to the first two is yes, and the papers to cite are Pex and Fox–Wei. The answer to the third is the open institutional debt catalogued in Section 10 of the original article.

## 3. Pex: The Engineering Counterexample

Pex is a white-box test generation tool built at Microsoft Research \[3\]. It applies dynamic symbolic execution, powered by the Z3 constraint solver, to .NET programs annotated as parameterized unit tests. A parameterized unit test is an ordinary method with free parameters and assertions; Pex treats the parameters as symbolic, explores the program's execution tree, and computes concrete inputs that steer execution along distinct paths.

The case study is the one the rebuttal needs to engage. A core .NET component, previously tested for approximately five years by forty testers and relied upon by thousands of developers and millions of end users, was analyzed by Pex running on roughly ten machines for three days. Pex achieved about 43% block coverage and 36% arc coverage fully automatically. It found a category of seventeen unique errors involving assertion violations, memory exhaustion, and other serious issues. As the authors note: "Most of the errors that Pex found required very carefully chosen argument values (e.g. a string of length 100 filled with particular characters), and it is unlikely that a random test input generator would find them."

Three features of Pex directly engage the rebuttal:

**It does not claim to solve undecidable problems.** The Pex authors are explicit: "Deciding reachability of program statements is a hard problem. In a system with dynamic class loading and virtual method dispatch the problem does not become easier." Pex's answer is not a decision procedure; it is a search strategy that achieves high coverage fast, under a user-specified time budget. This is precisely how civil engineering verifies structures: not by deciding a theoretically undecidable problem, but by bounding uncertainty within engineering tolerances.

**It produces evidence that was previously invisible.** The seventeen errors found were, by the rebuttal's implicit standard, in code that had been "verified" by five years of manual testing. The verification was incomplete; Pex found what manual testing missed. The existence of Pex demonstrates that software verification is not structurally blocked; it is operationally under-invested.

**It is readable as well as formal.** A `[PexMethod]` is an ordinary C# method with parameters and assertions. The same artifact is readable by a developer and consumable by a symbolic executor with Z3. The "legibility gap" is falsified at the code level exactly as Event Modeling falsifies it at the specification level: one artifact, two audiences.

## 4. Fox and Wei: The Mathematical Counterexample

If Pex is the engineering counterexample, Fox and Wei's "Fast property testing and metrics for permutations" \[4\] is the mathematical one. They prove that for every hereditary property of permutations, a two-sided tester exists with **query complexity polynomial in 1/ε and independent of the permutation's length**.

Specifically (Theorem 1.8): the universal bound is M = 20000/ε², meaning the tester samples 20000/ε² positions regardless of whether the permutation has a thousand or a trillion elements. If the permutation satisfies the property, the tester accepts with probability at least 1 − ε; if the permutation is ε-far from the property under the planar tau distance, the tester rejects with the same probability. The sample size is independent of the input size. That is the precise mathematical condition under which sampling-based verification works.

This is the theorem behind steel. It is the formal answer to "state spaces are effectively infinite." The state space of a permutation of length n is n!, which grows faster than anything civil engineering ever contemplates. Fox and Wei prove that hereditary properties (closed under sub-permutations, analogous to structural properties that hold for substructures) can be certified with constant-in-n query complexity and bounded error probability. Scale is not the obstacle; the property class and the tolerance ε are.

The specific result is about permutations, but the research programme it belongs to, **property testing**, has produced analogous results for graphs, boolean functions, algebraic properties, and metric spaces \[6, 7, 8\]. The rebuttal's appeal to "effectively infinite state spaces" reads Rice's theorem as disabling. Property testing shows that, for broad classes of properties, state space size is irrelevant; only ε, the confidence level, and the property class determine the sampling effort.

## 5. Rice's Theorem Does Not Forbid Engineering Verification

Rice's theorem states that every non-trivial semantic property of a program's function is undecidable. It is true, important, and routinely invoked as a disabling constraint on software verification. It is not a disabling constraint. It is a statement about decidability in the general case.

Rice's theorem does not forbid:

- **Approximate verification within bounded error.** Pex demonstrates this operationally; Fox and Wei demonstrate it mathematically.
- **Decidable sub-problems.** Most properties engineers care about (absence of null dereferences on specific paths, bounds on specific loop iterations, conformance of a specific trace to a specific specification) are decidable with finite resources.
- **Probabilistic guarantees.** Property testing produces probabilistic guarantees, which are the standard in every engineering discipline. Civil engineers do not claim zero probability of collapse; they claim collapse probabilities below regulatory thresholds, bounded by material sampling rates and safety factors in the code.

The analogous error in civil engineering would read: "Materials science is impossible because atoms are quantum-mechanical and we cannot compute the wave function of a steel beam." True, and irrelevant. The engineering technique operates at a level of abstraction where the quantum details average out into statistical material properties. Software verification operates at a level of abstraction where undecidable properties are approximated within bounded error, using samples, symbolic reasoning, and assertions.

## 6. The Other Three Claims

Once the verification claim is answered, the remaining rebuttal claims contract to engineering considerations.

**Physics vs. human meaning.** Software models human intent, which is volatile; civil engineering models physical loads, which are stable. True, but this is an argument about the *distribution* of change, not about whether verification is possible. The response to volatile inputs is not to abandon verification; it is to make verification cheap enough to run continuously. Fox and Wei's constant-in-n sample complexity is exactly the property that enables continuous verification. When re-verifying a changed specification costs O(1/ε²) samples, volatility is affordable.

**Requirements volatility.** The rebuttal frames this as disabling. The original article frames it as the economic case for upstream experimentation (Section 4.3, citing Thomke). Both framings are consistent with the engineering thesis: volatility rewards disciplines that verify cheaply and punishes disciplines that verify expensively. Pex and property testing are cheap verification; manual testing by forty testers over five years is expensive verification.

**Legibility gap.** Pex's `[PexMethod]` is formal (symbolic execution with Z3) and legible (ordinary C#) in one artifact. Event Modeling's Given-When-Then slices are formal (deterministic specifications verifiable by Deciders) and legible (readable by product managers) in one artifact. The historical claim that formal notations have failed is an observation about TLA+, Z, and B; it is not a claim that future formal notations must fail. The counterexamples already exist.

## 7. The Naur Challenge Resolves in Three Steps

Naur's "Programming as Theory Building" \[2\] argues that programming's essence is the construction of a mental theory, and that this theory cannot be fully externalized. The rebuttal uses this to claim autonomous agents produce "programs born dead" because they lack the theory.

Three observations suffice:

**The strong form is falsified by observation.** Linux, the JVM, the Python interpreter, and hundreds of other long-lived codebases continue to evolve correctly after their original authors have moved on, retired, or died. If the theory literally could not be externalized, this would be impossible. What is externalized is partial, but it is enough for continued correct evolution. The medium of externalization includes code, comments, commit messages, issue trackers, test suites, and institutional memory. None of these is "the theory" in Naur's strict sense; all of them together are the engineering substitute.

**The weak form is already conceded.** Section 10.4 of the original article (the emergent complexity boundary) acknowledges that some judgment may resist full formalization. The weak form of Naur, that some theory remains with humans, is consistent with the article's thesis: agents operate on well-specified slices, humans retain judgment on cross-slice and emergent concerns.

**Pex and Fox–Wei externalize the verification theory without externalizing the design theory.** Pex tests code against assertions; Fox–Wei tests permutations against hereditary properties. Neither requires that the author's "theory of the program" be fully externalized. They require only that the *property to verify* be formalized. This is a much weaker requirement, and it is the requirement engineering disciplines actually impose. A civil engineer does not need the architect's complete aesthetic theory to verify that a beam will not buckle.

## 8. Conclusion

The rebuttal's title is evocative and its argument is serious, but its load-bearing claim rests on a reading of Rice's theorem that engineering disciplines have never accepted. Civil engineering itself does not have what the rebuttal grants it: tensile strength is sampled, not measured; structural analysis is modelled, not computed against reality; material properties are statistically certified, not physically known. What civil engineering has is engineering technique under bounded uncertainty, and software has the analogous technique in Pex, property testing, and the broader research programme in lightweight formal methods.

The disanalogy the rebuttal proposes is not between civil engineering and software. It is between **decidable verification**, which neither discipline possesses, and **bounded-confidence verification**, which both disciplines possess. Software's gap is not in the underlying method; it is in the institutional infrastructure catalogued in Section 10 of the original article: licensure, education, industry standards. Those gaps are real, and closing them is the work ahead. But they are engineering gaps, not ontological ones.

Steel is sampled, too. Software can be.

---

## References

\[1\] C. Jørgensen, "No Tensile Strength to Measure," no-tensile-strength-to-measure.netlify.app, 2026. Rebuttal to "Software Civil Engineering" arguing four structural disanalogies between civil engineering and agentic software development.

\[2\] P. Naur, "Programming as Theory Building," *Microprocessing and Microprogramming*, vol. 15, no. 5, pp. 253–261, 1985. Argues that programming is the construction of a mental theory that cannot be fully externalized into specifications.

\[3\] N. Tillmann and J. de Halleux, "Pex: White Box Test Generation for .NET," in *TAP 2008*, LNCS 4966, B. Beckert and R. Hähnle, Eds. Berlin: Springer-Verlag, 2008, pp. 134–153. Demonstrates dynamic symbolic execution with Z3 applied to a core .NET component previously tested for five years by forty testers, finding seventeen unique errors including a serious issue at 43% block coverage, fully automatically.

\[4\] J. Fox and F. Wei, "Fast property testing and metrics for permutations," arXiv:1611.01270v2 \[math.CO\], 2018. Proves that hereditary permutation properties are two-sided testable with query complexity polynomial in 1/ε and independent of input size; the universal bound is M = 20000/ε² (Theorem 1.8).

\[5\] M. Rosén-Lidholm, "Software Civil Engineering: From Craft to Discipline," 2026. The original article to which the rebuttal responds, and to which this reply is, in turn, a response.

\[6\] R. Rubinfeld and M. Sudan, "Robust characterizations of polynomials with applications to program testing," *SIAM Journal on Computing*, vol. 25, no. 2, pp. 252–271, 1996. Foundational paper in property testing.

\[7\] O. Goldreich, S. Goldwasser, and D. Ron, "Property testing and its connection to learning and approximation," *Journal of the ACM*, vol. 45, no. 4, pp. 653–750, 1998. Initiates property testing for combinatorial objects.

\[8\] N. Alon and A. Shapira, "A characterization of the (natural) graph properties testable with one-sided error," *SIAM Journal on Computing*, vol. 37, no. 6, pp. 1703–1727, 2008. Every hereditary graph property is one-sided testable.
