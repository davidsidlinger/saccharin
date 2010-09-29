using System.Linq;

namespace Saccharin.CommandLine
{
	///<summary>
	/// Represents a positioned argument
	///</summary>
	public interface IPositioned : IArgument
	{
		///<summary>
		///  Gets the position of the <see cref = "Argument{TArgument}" /> in the argument list
		///</summary>
		int Position { get; }

		///<summary>
		/// Converts the named argument to a new value type
		///</summary>
		///<typeparam name="TTarget">The target type</typeparam>
		///<returns>A converted <see cref="INamed"/></returns>
		IPositioned<TTarget> ToPositioned<TTarget>();
	}

	///<summary>
	///</summary>
	///<typeparam name="TArgument"></typeparam>
	public interface IPositioned<out TArgument> : IPositioned, IArgument<TArgument> {}
}
