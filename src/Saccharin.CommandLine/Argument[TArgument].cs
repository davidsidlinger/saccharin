using System;
using System.Globalization;
using System.Linq;

namespace Saccharin.CommandLine
{
	///<summary>
	///  Represents a command-line argument
	///</summary>
	///<typeparam name = "TArgument">The value of the <see cref = "Argument{TArgument}" /></typeparam>
	public abstract class Argument<TArgument> : Argument, IArgument<TArgument>
	{
		private readonly TArgument _value;

		/// <summary>
		///   Initializes a new instance of the <see cref = "Argument&lt;TArgument&gt;" /> class.
		/// </summary>
		/// <param name = "value">The value.</param>
		protected Argument(TArgument value)
		{
			_value = value;
		}

		///<summary>
		/// Gets the value of the argument as a string
		///</summary>
		public override string StringValue
		{
			get { return string.Format(CultureInfo.InvariantCulture, "{0}", Value); }
		}

		///<summary>
		/// Gets the undelying type of the argument value
		///</summary>
		public override Type ValueType
		{
			get { return typeof(TArgument); }
		}

		#region IArgument<TArgument> Members

		///<summary>
		///  Gets the value of the <see cref = "Argument{TArgument}" />
		///</summary>
		public virtual TArgument Value
		{
			get { return _value; }
		}

		#endregion
	}
}
