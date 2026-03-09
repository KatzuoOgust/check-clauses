namespace CheckClauses
{
	/// <summary>
	/// Generic check clause interface with a value.
	/// </summary>
	/// <typeparam name="T">The type of the value being checked.</typeparam>
	public interface ICheckClause<T> : ICheckClause
	{
		/// <summary>
		/// Gets the value being checked.
		/// </summary>
		T Value { get; }
	}
}
