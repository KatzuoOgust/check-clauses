namespace CheckClauses.Examples;

/// <summary>
/// Example program demonstrating password validation using CheckClauses.
/// </summary>
class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("CheckClauses Examples");
		Console.WriteLine("====================\n");

		// Validate a password with multiple requirements
		var isValid = Check.That.Password("MyP@ssw0rd123")
			.Match(pwd => pwd.MeetsLengthRequirements(8, 20) 
				&& pwd.ContainsAtLeastCharacters(1, char.IsUpper) 
				&& pwd.ContainsAtLeastCharacters(1, char.IsLower) 
				&& pwd.ContainsAtLeastCharacters(1, char.IsDigit) 
				&& pwd.ContainsAtLeastCharacters(1, c => !char.IsLetterOrDigit(c))
			);

		Console.WriteLine(isValid ? "✓ Valid password passed all checks" : "✗ Password validation failed");
	}
}
