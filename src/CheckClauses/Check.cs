namespace KatzuoOgust.CheckClauses;

/// <summary>
/// Entry point for creating check clauses.
/// </summary>
public sealed class Check : ICheckClause
{
	/// <summary>
	/// Gets the singleton instance for starting check clauses.
	/// </summary>
	public static ICheckClause That { get; } = new Check();

	private Check()
	{
	}
}
