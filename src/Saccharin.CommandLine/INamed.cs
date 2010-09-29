using System.Linq;

namespace Saccharin.CommandLine
{
	/// <summary>
	/// </summary>
	public interface INamed : IArgument
	{
		/// <summary>
		///   Gets the name.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; }

		///<summary>
		/// Gets whether the named parameter had two dashes
		///</summary>
		bool IsDoubleDashed { get; }

		///<summary>
		/// Converts the named argument to a new value type
		///</summary>
		///<typeparam name="TTarget">The target type</typeparam>
		///<returns>A converted <see cref="INamed"/></returns>
		INamed<TTarget> ToNamed<TTarget>();
	}

	///<summary>
	///</summary>
	///<typeparam name="TArgument">The type of the named argument</typeparam>
	public interface INamed<out TArgument> : INamed, IArgument<TArgument> {}
}
