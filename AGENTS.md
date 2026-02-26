# Shintio.DependencyInjection AGENTS.md file

## Project overview
Shintio.DependencyInjection is a lightweight DI abstraction layer that lets library/application code register services through a single contract (`IServiceRegistrar`) and then map those registrations to concrete containers (Microsoft DI, Slim container, Autofac, Lamar, SimpleInjector, Unity-oriented adapters).
The project goal is API-level portability between DI providers without leaking provider-specific types into consumer code.

## Project structure
- `src/Shintio.DependencyInjection.Abstractions/` - core public contract: `IServiceRegistrar`, `ServiceProviderProxy`, and extension APIs used by consumers.
- `src/Shintio.DependencyInjection.Container.Slim/` - minimal in-house container implementation (`ServiceCollection`, `ServiceProvider`) used directly and by Slim adapter.
- `src/Shintio.DependencyInjection.Adapter.Autofac/` - adapter from abstraction API to Autofac (`ContainerBuilder` registrations).
- `src/Shintio.DependencyInjection.Adapter.Lamar/` - adapter from abstraction API to Lamar (`ServiceRegistry` registrations).
- `src/Shintio.DependencyInjection.Adapter.Microsoft/` - adapter from abstraction API to `Microsoft.Extensions.DependencyInjection`.
- `src/Shintio.DependencyInjection.Adapter.SimpleInjector/` - adapter from abstraction API to SimpleInjector (`Container` registrations).
- `src/Shintio.DependencyInjection.Adapter.Slim/` - adapter from abstraction API to Slim container APIs.
- `src/Shintio.DependencyInjection.Adapter.Unity.VContainer/` - adapter for Unity VContainer.
- `src/Shintio.DependencyInjection.Adapter.Unity.Zenject/` - Unity Zenject adapter area.
- `src/Shintio.DependencyInjection.Adapter.Unity.Reflex/` - Unity Reflex adapter area.
- `sandbox/` - console playground projects (`TestSlimApp`, `TestMicrosoftApp`, `TestLib`) for manual verification/examples.
- `tests/Shintio.DependencyInjection.Tests/` - shared NUnit test kit (`AdapterTestsBase`, common test types) and Unity-local package source for shared tests.
- `tests/Shintio.DependencyInjection.Tests.Standalone/` - .NET adapter test runner project (inherits shared test base).
- `tests/Shintio.DependencyInjection.Tests.Unity/` - Unity test project (EditMode tests for Unity adapters).

## Abstraction contract policy
- `IServiceRegistrar` is the source-of-truth API. Keep signatures stable and evolution additive where possible.
- `ServiceRegistrarExtensions` must stay convenience-only; do not hide semantic differences there.
- `ServiceProviderProxy` must remain container-agnostic and minimal (resolve/get-required semantics only).
- Any new registration shape added to abstractions must be implemented consistently in all active adapters or explicitly documented as unsupported.

## Lifetime and behavior parity
- The same registration chain should behave as similarly as possible across adapters.
- Pay special attention to `Transient`, `Scoped`, and `Singleton` semantics; avoid implicit downgrade of lifetimes.
- For providers that require explicit scope activation (for example, Microsoft DI and SimpleInjector), run scoped behavior checks inside an active scope.
- If a provider cannot support a feature fully, fail explicitly or document the limitation near adapter code and in this file.
- Constructor resolution behavior in Slim should stay deterministic and predictable.

## Adapter implementation rules
- Keep provider-specific types inside adapter projects only.
- Avoid referencing Microsoft DI or Unity types from `Abstractions` and `Container.Slim`.
- Keep package/framework constraints explicit per adapter (example: Lamar package currently targets modern .NET only, not `netstandard2.1`).
- Unity adapters should avoid hard machine-local dependencies in project files; prefer portable references/workflows.
- Zenject/Reflex/VContainer adapter parity should be tracked intentionally (do not leave silent partial implementations).

## Testing conventions
- Primary test framework is `NUnit` for both standalone .NET and Unity parity.
- Standalone adapter fixtures should use `[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]` to avoid container reuse between test methods.
- Unity NUnit runner does not support `FixtureLifeCycle(LifeCycle.InstancePerTestCase)` consistently; in Unity tests reset container state via `[SetUp]` and create fresh builders/scopes there.
- Shared adapter behavior tests belong in `tests/Shintio.DependencyInjection.Tests/AdapterTestsBase.cs`.
- Adapter-specific tests should stay thin and only provide container wiring (`RegisterServices`, `BuildProvider`).
- Unity EditMode assembly currently targets:
  - `Shintio.DependencyInjection.Adapter.Unity.VContainer`
  - `Shintio.DependencyInjection.Adapter.Unity.Reflex`

## When changing functionality
- Abstraction changes: update adapters + sandbox usage examples + tests in the same task.
- Slim container changes: verify constructor selection, recursion behavior, missing dependency errors, and caching/lifetime behavior.
- Adapter changes: validate at least one end-to-end registration flow from `sandbox/TestLib`.
- Any architecture/process change must update this `AGENTS.md`.

## Unity package wiring
- Unity-facing projects must include:
  - `package.json`
  - `.asmdef`
  - `Directory.Build.props`
  - `Directory.Build.targets`
- Local Unity dependencies should be referenced via `file:` paths to repo projects (prefer `src/...` and `tests/...` local packages).
- Keep Unity-generated solution/project artifacts out of source-of-truth design decisions; authoritative config is `Packages/manifest.json`, package metadata, and asmdefs.

## Build and test workflow
- Build from repo root:
  `dotnet build Shintio.DependencyInjection.slnx`
- Run tests from repo root:
  `dotnet test Shintio.DependencyInjection.slnx`
- Sandbox projects are for quick checks, but they do not replace automated tests under `tests/`.

## Environment

### WSL
- For .NET commands from WSL, prefer:
  `cmd.exe /C "dotnet ..."`
- For git commands from WSL, prefer:
  `cmd.exe /C "git ..."`

### Windows
- Run commands from repository root when building/testing all projects via solution file.

## Code style

### C#
- Indent with tabs (size 4), no mixed tabs/spaces.
- Trim trailing whitespace.
- Nullable reference types enabled.
- Implicit usings disabled.
- Keep access modifiers explicit.
- Prefer one top-level class per file unless there is a strong reason not to.

## Agent's Specific Rules

### Antigravity
- Skip Plan Review: Always proceed to EXECUTION immediately after creating a plan, without waiting for user confirmation, unless the task is extremely high-risk.
