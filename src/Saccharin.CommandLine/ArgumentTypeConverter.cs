using System;
using System.Linq;

namespace Saccharin.CommandLine
{
	///<summary>
	/// Converts arguments to a given type
	///</summary>
	///<typeparam name="TTarget">The target type of the argument</typeparam>
	///<typeparam name="TArgument">The original argument type</typeparam>
	public class ArgumentTypeConverter<TArgument, TTarget>
	{
		private readonly Converter<TArgument, TTarget> _converter;

		///<summary>
		/// Initializes a new instance of <see cref="ArgumentTypeConverter{TArgument,TTarget}"/>
		///</summary>
		///<param name="converter">The delegate to use for conversion</param>
		public ArgumentTypeConverter(Converter<TArgument, TTarget> converter)
		{
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			_converter = converter;
		}

		///<summary>
		/// Initializes a new instance of <see cref="ArgumentTypeConverter{TArgument,TTarget}"/>
		///</summary>
		public ArgumentTypeConverter() : this(value => (TTarget)System.Convert.ChangeType(value, typeof(TTarget))) {}

		///<summary>
		/// Converts an argument to the desired type
		///</summary>
		///<param name="named">The <see cref="NamedArgument{TArgument}"/> argument to convert</param>
		///<returns>A <see cref="NamedArgument{TArgument}"/> with the converted value</returns>
		public virtual NamedArgument<TTarget> Convert(NamedArgument<TArgument> named)
		{
			if (named == null)
			{
				throw new ArgumentNullException("named");
			}
			return new NamedArgument<TTarget>(named.Name, named.IsDoubleDashed, _converter(named.Value));
		}

		///<summary>
		/// Converts an argument to the desired type
		///</summary>
		///<param name="positioned">The <see cref="PositionedArgument{TArgument}"/> argument to convert</param>
		///<returns>A <see cref="NamedArgument{TArgument}"/> with the converted value</returns>
		public virtual PositionedArgument<TTarget> Convert(PositionedArgument<TArgument> positioned)
		{
			if (positioned == null)
			{
				throw new ArgumentNullException("positioned");
			}
			return new PositionedArgument<TTarget>(positioned.Position, _converter(positioned.Value));
		}
	}
}
