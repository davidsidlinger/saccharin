using System;
using System.Linq;

namespace Saccharin.CommandLine
{
	///<summary>
	///  Represents a named arument
	///</summary>
	public class NamedArgument<TArgument> : Argument<TArgument>, INamed<TArgument>
	{
		private readonly string _name;
		private readonly bool _isDoubleDashed;

		/// <summary>
		///   Initializes a new instance of the <see cref = "NamedArgument&lt;TArgument&gt;" /> class.
		/// </summary>
		/// <param name = "name">The name.</param>
		/// <param name="isDoubleDashed"></param>
		/// <param name="value">The value.</param>
		public NamedArgument(string name, bool isDoubleDashed, TArgument value) : base(value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentOutOfRangeException("name", name, "Name cannot be null or empty.");
			}
			_name = name;
			_isDoubleDashed = isDoubleDashed;
		}

		#region INamed<TArgument> Members

		///<summary>
		///  Gets the name of the argument
		///</summary>
		public string Name
		{
			get { return _name; }
		}

		///<summary>
		/// Gets whether the named parameter had two dashes
		///</summary>
		public bool IsDoubleDashed
		{
			get { return _isDoubleDashed; }
		}

		///<summary>
		/// Converts the named argument to a new value type
		///</summary>
		///<typeparam name="TTarget">The target type</typeparam>
		///<returns>A converted <see cref="INamed"/></returns>
		public INamed<TTarget> ToNamed<TTarget>()
		{
			return new ArgumentTypeConverter<TArgument, TTarget>().Convert(this);
		}

		///<summary>
		/// Converts the argument to a new value type
		///</summary>
		///<typeparam name="TTarget">The target type</typeparam>
		///<returns>A converted <see cref="IArgument{TArgument}"/></returns>
		public override IArgument<TTarget> To<TTarget>()
		{
			return ToNamed<TTarget>();
		}

		#endregion
	}
}
