using System;
using System.Linq;

namespace Saccharin.CommandLine
{
	///<summary>
	///  Represents a command-line argument
	///</summary>
	public abstract class Argument : IArgument
	{
		#region IArgument Members

		///<summary>
		/// Gets the value of the argument as a string
		///</summary>
		public abstract string StringValue { get; }

		///<summary>
		/// Gets the undelying type of the argument value
		///</summary>
		public abstract Type ValueType { get; }

		///<summary>
		/// Converts the argument to a new value type
		///</summary>
		///<typeparam name="TTarget">The target type</typeparam>
		///<returns>A converted <see cref="IArgument{TArgument}"/></returns>
		public abstract IArgument<TTarget> To<TTarget>();

		#endregion
	}
}
