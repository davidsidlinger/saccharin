using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Saccharin.CommandLine
{
	/// <summary>
	///   A <see cref = "ArgumentDescription" /> for a positioned argument
	/// </summary>
	public class PositionedArgumentDescription<TTarget> : ArgumentDescription<TTarget>
	{
		private readonly int _position;

		/// <summary>
		///   Initializes a new instance of the <see cref = "NamedArgumentDescription{TTarget}" /> class.
		/// </summary>
		/// <param name = "position">The position.</param>
		public PositionedArgumentDescription(int position)
		{
			_position = position;
		}

		///<summary>
		/// Gets the position described.
		///</summary>
		public virtual int Position
		{
			get { return _position; }
		}

		///<summary>
		/// Rewrites the <see cref="Argument"/> enumeration, based on the description given.
		///</summary>
		///<param name="continuation">The <see cref="Action{T1, T2}"/> to invoke with the rewritten arguments and validation messages</param>
		///<param name="arguments">An <see cref="IEnumerable{T}"/> of <see cref="Argument"/> to be examined</param>
		///<param name="messages">A <see cref="ICollection{T}"/> of validation messages</param>
		public override void RewriteArguments(Action<IEnumerable<Argument>, IEnumerable<string>> continuation, IEnumerable<Argument> arguments, IEnumerable<string> messages)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			if (arguments == null || arguments.Any(a => a == null))
			{
				throw new ArgumentNullException("arguments");
			}
			if (messages == null)
			{
				throw new ArgumentNullException("messages");
			}

			var argumentsLookedAt = from asPositioned in arguments.Select(a => new { Argument = a, Positioned = a as IPositioned })
															select
																new
																{
																	Care = asPositioned.Positioned != null && asPositioned.Positioned.Position == Position,
																	asPositioned.Argument,
																	asPositioned.Positioned
																};

			var argumentsToValidate = argumentsLookedAt
				.Where(a => a.Care)
				.Select(a => a.Positioned)
				.ToList();

			var count = argumentsToValidate.Count();
			var messagesCreated = new List<string>();

			if(count > 1)
			{
				var message = string.Format(CultureInfo.CurrentCulture,
				                            "{0} arguments at position {1}. Something is wrong.",
				                            count,
				                            Position);
				messagesCreated.Add(message);
			}

			if(IsRequired && count == 0)
			{
				var message = string.Format(CultureInfo.CurrentCulture,
																		"Missing required argument at position {0}.",
																		Position);
				messagesCreated.Add(message);
			}

			if(!string.IsNullOrEmpty(MatchPattern))
			{
				var regex = new Regex(MatchPattern);
				messagesCreated.AddRange(
					argumentsToValidate
					.OfType<PositionedArgument<string>>()
					.Where(a => !regex.IsMatch(a.Value))
					.Select(
					        a =>
					        string.Format(CultureInfo.CurrentCulture,
					                      "'{0}' is an invalid value for the argument at position {1}.",
					                      a.Value,
					                      Position)));
			}

			var others = argumentsLookedAt.Where(a => !a.Care).Select(a => a.Argument);
			var convertedAsArgs = argumentsToValidate.Select(a =>
			                                                 {
																												 try
																												 {
																												 	return a.To<TTarget>();
																												 }
																												 catch(FormatException)
																												 {
																												 	throw new NotImplementedException();
																												 }
																												 catch(InvalidCastException)
																												 {
																												 	throw new NotImplementedException();
																												 }
			                                                 }).Cast<Argument>();

			continuation(others.Concat(convertedAsArgs), messages.Concat(messagesCreated.AsEnumerable()));
		}
	}
}
