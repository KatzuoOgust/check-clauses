using System;

namespace CheckClauses
{
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
	}
}
