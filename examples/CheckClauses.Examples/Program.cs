namespace KatzuoOgust.CheckClauses.Examples;

/// <summary>
/// Example program demonstrating password validation using CheckClauses.
/// </summary>
class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("CheckClauses Examples");
		Console.WriteLine("====================\n");

		// Example 1: Match - returns bool
		Console.WriteLine("Example 1: Match (returns bool)");
		var isValid = Check.That.Password("MyP@ssw0rd123")
			.Match(pwd => pwd.MeetsLengthRequirements(8, 20) 
				&& pwd.ContainsAtLeastCharacters(1, char.IsUpper) 
				&& pwd.ContainsAtLeastCharacters(1, char.IsLower) 
				&& pwd.ContainsAtLeastCharacters(1, char.IsDigit) 
				&& pwd.ContainsAtLeastCharacters(1, c => !char.IsLetterOrDigit(c))
			);
		Console.WriteLine(isValid ? "✓ Valid password passed all checks" : "✗ Password validation failed");
		Console.WriteLine();

		// Example 2: MatchOrThrow with custom static message
		Console.WriteLine("Example 2: MatchOrThrow with custom message");
		try
		{
			Check.That.Password("weak")
				.MatchOrThrow(
					pwd => pwd.MeetsLengthRequirements(8, 20),
					"Password must be between 8 and 20 characters"
				);
			Console.WriteLine("✓ Password passed validation");
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine($"✗ Validation failed: {ex.Message}");
		}
		Console.WriteLine();

		// Example 3: MatchOrThrow with dynamic message based on clause
		Console.WriteLine("Example 3: MatchOrThrow with dynamic message");
		try
		{
			Check.That.Password("short")
				.MatchOrThrow(
					pwd => pwd.MeetsLengthRequirements(8, 20),
					pwd => $"Password '{pwd.Value}' does not meet length requirements (8-20 characters)"
				);
			Console.WriteLine("✓ Password passed validation");
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine($"✗ Validation failed: {ex.Message}");
		}
	}
}
