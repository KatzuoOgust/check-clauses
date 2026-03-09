namespace CheckClauses.Examples
{
	/// <summary>
	/// Check clause for password validation.
	/// </summary>
	public class PasswordCheckClause : ICheckClause<string>
	{
		/// <summary>
		/// Gets the password value being checked.
		/// </summary>
		public string? Value { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PasswordCheckClause"/> class.
		/// </summary>
		/// <param name="password">The password to check.</param>
		public PasswordCheckClause(string? password)
		{
			Value = password;
		}
	}
}
