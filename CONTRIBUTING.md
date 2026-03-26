# Contributing to CheckClauses

Fork the repo → create a branch → make your changes → run tests → open a PR against `main`.

## Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- `make` (optional, for convenience targets)

## Local Setup

```bash
git clone https://github.com/KatzuoOgust/check-clauses.git
cd check-clauses
dotnet restore CheckClauses.sln
```

## Common Commands

```bash
make build    # Build the solution
make test     # Run tests
make run      # Run the examples project
make pack     # Create a NuGet package (Release)
```

Or with the .NET CLI directly:

```bash
dotnet build CheckClauses.sln
dotnet test CheckClauses.sln
dotnet run --project examples/CheckClauses.Examples/CheckClauses.Examples.csproj
```

## Code Style

The CI pipeline enforces formatting with `dotnet format`. Run it locally before pushing:

```bash
dotnet format CheckClauses.sln --verify-no-changes
```

Fix any formatting issues with:

```bash
dotnet format CheckClauses.sln
```

## Branching

- `main` — integration and release branch; open PRs here

Use short, descriptive branch names: `feature/add-string-clause`, `fix/null-check-edge-case`.

## Pull Requests

- PRs must pass all CI checks (build, format, pack) on Ubuntu, Windows, and macOS.
- Include tests for new validation logic.
- Keep changes focused — one feature or fix per PR.

## What Not to Contribute

- Changes to auto-generated or tool-managed files (e.g., `*.Designer.cs`, `bin/`, `obj/`)
- New dependencies without prior discussion in an issue
- Breaking changes to the public API without a corresponding version bump discussion

## Releasing

See [docs/RELEASE_GUIDE.md](docs/RELEASE_GUIDE.md) for the full release process.
