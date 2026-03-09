using System;
using System.Linq;

namespace CheckClauses.Examples
{
	/// <summary>
	/// Extension methods for password check clauses.
	/// </summary>
	public static class PasswordCheckClauseExtensions
	{
		/// <summary>
		/// Creates a password check clause.
		/// </summary>
		/// <param name="clause">The check clause.</param>
		/// <param name="password">The password to check.</param>
		/// <returns>A password check clause.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="clause"/> is null.</exception>
		public static PasswordCheckClause Password(this ICheckClause clause, string password)
		{
			if (clause == null) throw new ArgumentNullException(nameof(clause));

			return new PasswordCheckClause(password);
		}

		/// <summary>
		/// Checks if the password meets length requirements.
		/// </summary>
		/// <param name="clause">The password check clause.</param>
		/// <param name="min">Minimum length.</param>
		/// <param name="max">Maximum length.</param>
		/// <returns>The password check clause.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="clause"/> is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when min or max are invalid.</exception>
		public static bool MeetsLengthRequirements(this PasswordCheckClause clause, int min, int max)
		{
			if (clause == null) throw new ArgumentNullException(nameof(clause));
			if (min < 0) throw new ArgumentOutOfRangeException(nameof(min));
			if (max < min) throw new ArgumentOutOfRangeException(nameof(max));

			var length = clause.Value?.Length ?? 0;
			if (length < min || length > max)
				return false;

			return true;
		}

		/// <summary>
		/// Checks if the password contains at least one character matching the tester.
		/// </summary>
		/// <param name="clause">The password check clause.</param>
		/// <param name="tester">Function to test characters.</param>
		/// <returns>The password check clause.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="clause"/> or <paramref name="tester"/> is null.</exception>
		public static bool ContainsAtLeastCharacters(this PasswordCheckClause clause, int count, Func<char, bool> tester)
		{
			if (clause == null) throw new ArgumentNullException(nameof(clause));
			if (tester == null) throw new ArgumentNullException(nameof(tester));

			if (string.IsNullOrEmpty(clause.Value) || !clause.Value.Any(tester))
				return false;

			return true;
		}
	}
}
