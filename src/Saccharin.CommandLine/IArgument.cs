using System;
using System.Linq;

namespace Saccharin.CommandLine
{
	/// <summary>
	/// 
	/// </summary>
	public interface IArgument<out TArgument>
	{
		///<summary>
		/// Gets the argument value
		///</summary>
		TArgument Value { get; }
	}

	///<summary>
	/// Empty interface for arguments
	///</summary>
	public interface IArgument
	{
		///<summary>
		/// Gets the value of the argument as a string
		///</summary>
		string StringValue { get; }

		///<summary>
		/// Gets the undelying type of the argument value
		///</summary>
		Type ValueType { get; }

		///<summary>
		/// Converts the argument to a new value type
		///</summary>
		///<typeparam name="TTarget">The target type</typeparam>
		///<returns>A converted <see cref="IArgument{TArgument}"/></returns>
		IArgument<TTarget> To<TTarget>();
	}
}
