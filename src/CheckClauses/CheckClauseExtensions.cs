using System;

namespace KatzuoOgust.CheckClauses;

/// <summary>
/// Extension methods for check clauses.
/// </summary>
public static class CheckClauseExtensions
{
	/// <summary>
	/// Applies a check function to the check clause.
	/// </summary>
	/// <typeparam name="T">The type of the check clause.</typeparam>
	/// <param name="clause">The check clause.</param>
	/// <param name="check">The check function to apply.</param>
	/// <returns>The check clause.</returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="clause"/> or <paramref name="check"/> is null.</exception>
	public static bool Match<T>(this T clause, Func<T, bool> check)
		where T : ICheckClause
	{
		if (clause == null) throw new ArgumentNullException(nameof(clause));
		if (check == null) throw new ArgumentNullException(nameof(check));

		return check(clause);
	}

	/// <summary>
	/// Applies a check function to the check clause and throws an exception if the check fails.
	/// </summary>
	/// <typeparam name="T">The type of the check clause.</typeparam>
	/// <param name="clause">The check clause.</param>
	/// <param name="check">The check function to apply.</param>
	/// <returns>The check clause for method chaining.</returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="clause"/> or <paramref name="check"/> is null.</exception>
	/// <exception cref="InvalidOperationException">Thrown when the check function returns false.</exception>
	public static T MatchOrThrow<T>(this T clause, Func<T, bool> check)
		where T : ICheckClause
	{
		return MatchOrThrow(clause, check, "Check clause validation failed.");
	}

	/// <summary>
	/// Applies a check function to the check clause and throws an exception with a custom message if the check fails.
	/// </summary>
	/// <typeparam name="T">The type of the check clause.</typeparam>
	/// <param name="clause">The check clause.</param>
	/// <param name="check">The check function to apply.</param>
	/// <param name="message">The error message to use when the check fails.</param>
	/// <returns>The check clause for method chaining.</returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="clause"/> or <paramref name="check"/> is null.</exception>
	/// <exception cref="InvalidOperationException">Thrown when the check function returns false.</exception>
	public static T MatchOrThrow<T>(this T clause, Func<T, bool> check, string message)
		where T : ICheckClause
	{
		if (clause == null) throw new ArgumentNullException(nameof(clause));
		if (check == null) throw new ArgumentNullException(nameof(check));

		if (!check(clause))
		{
			throw new InvalidOperationException(message);
		}

		return clause;
	}

	/// <summary>
	/// Applies a check function to the check clause and throws an exception with a dynamically generated message if the check fails.
	/// </summary>
	/// <typeparam name="T">The type of the check clause.</typeparam>
	/// <param name="clause">The check clause.</param>
	/// <param name="check">The check function to apply.</param>
	/// <param name="messageFactory">A function that generates the error message based on the clause.</param>
	/// <returns>The check clause for method chaining.</returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="clause"/>, <paramref name="check"/>, or <paramref name="messageFactory"/> is null.</exception>
	/// <exception cref="InvalidOperationException">Thrown when the check function returns false.</exception>
	public static T MatchOrThrow<T>(this T clause, Func<T, bool> check, Func<T, string> messageFactory)
		where T : ICheckClause
	{
		if (clause == null) throw new ArgumentNullException(nameof(clause));
		if (check == null) throw new ArgumentNullException(nameof(check));
		if (messageFactory == null) throw new ArgumentNullException(nameof(messageFactory));

		if (!check(clause))
		{
			throw new InvalidOperationException(messageFactory(clause));
		}

		return clause;
	}
}
