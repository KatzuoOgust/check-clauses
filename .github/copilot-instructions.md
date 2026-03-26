# Copilot Instructions

## Project Identity

C# .NET class library. Multi-target: .NET Standard 2.1 and .NET 8.0. No frontend, no database. Package manager: NuGet via `dotnet`.

## Commands

```bash
dotnet build CheckClauses.sln          # Build
dotnet test CheckClauses.sln           # Run tests
dotnet format CheckClauses.sln         # Fix formatting
dotnet format CheckClauses.sln --verify-no-changes  # Check formatting (CI)
dotnet pack src/CheckClauses/CheckClauses.csproj --configuration Release  # Pack NuGet
dotnet run --project examples/CheckClauses.Examples/CheckClauses.Examples.csproj  # Run examples
```

Or use `make` targets: `build`, `test`, `run`, `pack`, `clean`, `restore`.

## Architecture

The library is intentionally minimal:

- `ICheckClause` — marker interface; all check clauses implement this
- `ICheckClause<T>` — adds a typed `Value` property
- `Check` — sealed singleton; `Check.That` is the entry point
- `CheckClauseExtensions` — `Match<T>` and `MatchOrThrow<T>` overloads

Custom check clauses are added by consumers via extension methods — do not add built-in clause types to `src/CheckClauses/`. Place examples in `examples/` instead.

## Conventions

- Nullable reference types are enabled; respect nullability annotations
- Use `<LangVersion>latest</LangVersion>` features where appropriate
- Guard clauses use `ArgumentNullException` (not `ArgumentException`)
- Extension method parameters are named `clause`, not `this`, in XML docs
- Targets must remain multi-platform (no Windows-only APIs)

## Constraints

- Do not add NuGet dependencies to `src/CheckClauses/` — keep the core library dependency-free
- Do not modify `.github/workflows/` without reviewing versioning strategy in `docs/VERSION_PROPERTIES.md`
- `AssemblyVersion` is intentionally set to `MAJOR.0.0.0` for binary compatibility — do not change this pattern
- The `tests/` directory exists but currently has no test project; add one under `tests/CheckClauses.Tests/` if writing tests

## See Also

- [CONTRIBUTING.md](../CONTRIBUTING.md) — contributor workflow
- [docs/VERSION_PROPERTIES.md](../docs/VERSION_PROPERTIES.md) — versioning strategy
- [docs/RELEASE_GUIDE.md](../docs/RELEASE_GUIDE.md) — release process
