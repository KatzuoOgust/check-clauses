# CheckClauses

A fluent .NET library for building type-safe validation clauses using a declarative API.

## Features

- ✅ Fluent and expressive validation API
- ✅ Type-safe clause composition
- ✅ Extensible through custom check clauses
- ✅ Multi-target support: .NET Standard 2.1, .NET 8.0
- ✅ Null-safe with nullable reference types enabled

## Installation

```bash
dotnet add package CheckClauses
```

## Quick Start

```csharp
using KatzuoOgust.CheckClauses;
using KatzuoOgust.CheckClauses.Examples;

// Validate a password with multiple requirements
var isValid = Check.That.Password("MyP@ssw0rd123")
    .Match(pwd => pwd.MeetsLengthRequirements(8, 20) 
        && pwd.ContainsAtLeastCharacters(1, char.IsUpper) 
        && pwd.ContainsAtLeastCharacters(1, char.IsLower) 
        && pwd.ContainsAtLeastCharacters(1, char.IsDigit) 
        && pwd.ContainsAtLeastCharacters(1, c => !char.IsLetterOrDigit(c))
    );
```

## Core Concepts

### ICheckClause

The marker interface for all check clauses:

```csharp
public interface ICheckClause
{
}
```

### ICheckClause&lt;T&gt;

Generic check clause interface with a value:

```csharp
public interface ICheckClause<T> : ICheckClause
{
    T? Value { get; }
}
```

### Check.That

The entry point for creating check clauses:

```csharp
var clause = Check.That;
```

## Creating Custom Check Clauses

1. Create a class implementing `ICheckClause<T>`:

```csharp
public class PasswordCheckClause : ICheckClause<string>
{
    public string? Value { get; }

    public PasswordCheckClause(string? password)
    {
        Value = password;
    }
}
```

2. Add extension methods:

```csharp
public static class PasswordCheckClauseExtensions
{
    public static PasswordCheckClause Password(this ICheckClause clause, string password)
    {
        if (clause == null) throw new ArgumentNullException(nameof(clause));
        return new PasswordCheckClause(password);
    }

    public static bool MeetsLengthRequirements(this PasswordCheckClause clause, int min, int max)
    {
        if (clause == null) throw new ArgumentNullException(nameof(clause));
        var length = clause.Value?.Length ?? 0;
        return length >= min && length <= max;
    }
}
```

## Building

### Using Make

```bash
make build    # Build the solution
make clean    # Clean build artifacts
make restore  # Restore NuGet packages
make test     # Run tests
make run      # Run examples
make pack     # Create NuGet package
```

### Using .NET CLI

```bash
dotnet build CheckClauses.sln
dotnet test CheckClauses.sln
dotnet run --project examples/CheckClauses.Examples/CheckClauses.Examples.csproj
```

## Project Structure

```
├── src/
│   └── CheckClauses/          # Main library
│       ├── ICheckClause.cs
│       ├── ICheckClauseT.cs
│       ├── Check.cs
│       └── CheckClauseExtensions.cs
├── examples/
│   └── CheckClauses.Examples/ # Example implementations
│       ├── PasswordCheckClause.cs
│       └── PasswordCheckClauseExtensions.cs
├── tests/                     # Unit tests
└── CheckClauses.sln
```

## Target Frameworks

- .NET Standard 2.1
- .NET 8.0
- .NET 9.0 (when SDK available)
- .NET 10.0 (when SDK available)

## License

MIT

## Author

KatzuoOgust
