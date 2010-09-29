using System.Linq;

namespace Saccharin.CommandLine
{
	/// <summary>
	///   An argument with no name
	/// </summary>
	public class PositionedArgument<TArgument> : Argument<TArgument>, IPositioned<TArgument>
	{
		private readonly int _position;

		/// <summary>
		///   Initializes a new instance of the <see cref = "PositionedArgument{TArgument}" /> class.
		/// </summary>
		/// <param name = "position">The position.</param>
		/// <param name = "value">The value.</param>
		public PositionedArgument(int position, TArgument value) : base(value)
		{
			_position = position;
		}

		#region IPositioned<TArgument> Members

		///<summary>
		///  Gets the position of the <see cref = "Argument{TArgument}" /> in the argument list
		///</summary>
		public int Position
		{
			get { return _position; }
		}

		///<summary>
		/// Converts the argument to a new value type
		///</summary>
		///<typeparam name="TTarget">The target type</typeparam>
		///<returns>A converted <see cref="IPositioned{TArgument}"/></returns>
		public IPositioned<TTarget> ToPositioned<TTarget>()
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
			return ToPositioned<TTarget>();
		}

		#endregion
	}
}
