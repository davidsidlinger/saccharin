using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Saccharin.CommandLine
{
	/// <summary>
	///   A <see cref = "ArgumentDescription{TTarget}" /> for a named argument
	/// </summary>
	public class NamedArgumentDescription<TTarget> : ArgumentDescription<TTarget>
	{
		private readonly string _name;
		private readonly StringComparison _stringComparison;

		/// <summary>
		///   Initializes a new instance of the <see cref = "NamedArgumentDescription{TTarget}" /> class.
		/// </summary>
		/// <param name = "name">The name.</param>
		public NamedArgumentDescription(string name) : this(name, StringComparison.OrdinalIgnoreCase) {}

		/// <summary>
		///   Initializes a new instance of the <see cref = "NamedArgumentDescription{TTarget}" /> class.
		/// </summary>
		/// <param name = "name">The name.</param>
		/// <param name="stringComparison"></param>
		public NamedArgumentDescription(string name, StringComparison stringComparison)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentOutOfRangeException("name", name, "Name cannot be null or empty.");
			}
			_name = name;
			_stringComparison = stringComparison;
		}

		///<summary>
		/// Gets the name of the argument(s) being described.
		///</summary>
		public virtual string Name
		{
			get { return _name; }
		}

		/// <summary>
		///   Gets or sets a value indicating whether this instance is flag.
		/// </summary>
		/// <value><c>true</c> if this instance is flag; otherwise, <c>false</c>.</value>
		[SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flag")]
		public bool IsFlag { get; set; }

		/// <summary>
		/// Gets or sets whether multiple occu
		/// </summary>
		public bool AllowMultiple { get; set; }

		///<summary>
		/// Rewrites the <see cref="Argument"/> enumeration, based on the description given.
		///</summary>
		///<param name="continuation">The <see cref="Action{T1, T2}"/> to invoke with the rewritten arguments and validation messages</param>
		///<param name="arguments">An <see cref="IEnumerable{T}"/> of <see cref="Argument"/> to be examined</param>
		///<param name="messages">A <see cref="ICollection{T}"/> of validation messages</param>
		public override void RewriteArguments(Action<IEnumerable<Argument>, IEnumerable<string>> continuation,
		                                      IEnumerable<Argument> arguments,
		                                      IEnumerable<string> messages)
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

			var argumentsLookedAt = from asNamed in arguments.Select(a => new { Argument = a, Named = a as INamed })
			                        select
			                        	new
			                        	{
			                        		Care = asNamed.Named != null && asNamed.Named.Name.Equals(Name, _stringComparison),
			                        		asNamed.Argument,
			                        		asNamed.Named
			                        	};

			var argumentsToValidate = argumentsLookedAt
				.Where(a => a.Care)
				.Select(a => a.Named)
				.ToList();

			var count = argumentsToValidate.Count();
			var messagesCreated = new List<string>();

			if (IsRequired && count == 0)
			{
				var message = string.Format(CultureInfo.CurrentCulture, "'{0}' is a required argument.", Name);
				messagesCreated.Add(message);
			}

			if (!AllowMultiple && count > 1)
			{
				var message = string.Format(CultureInfo.CurrentCulture, "'{0}' is not allowed to occur multiple times.", Name);
				messagesCreated.Add(message);
			}

			if (!string.IsNullOrEmpty(MatchPattern))
			{
				var regex = new Regex(MatchPattern);
				messagesCreated.AddRange(
				                         argumentsToValidate
				                         	.OfType<NamedArgument<string>>()
				                         	.Where(a => !regex.IsMatch(a.Value))
				                         	.Select(
				                         	        a =>
				                         	        string.Format(CultureInfo.CurrentCulture,
				                         	                      "'{0}' is not a valid value for '{1}'",
				                         	                      a.Value,
				                         	                      a.Name)));
			}

			var others = argumentsLookedAt.Where(a => !a.Care).Select(a => a.Argument);
			var convertedAsArgs = argumentsToValidate.Select(a =>
			                                                 {
			                                                 	try
			                                                 	{
			                                                 		return a.To<TTarget>();
			                                                 	}
			                                                 	catch (FormatException)
			                                                 	{
			                                                 		throw new NotImplementedException();
			                                                 	}
			                                                 	catch (InvalidCastException)
			                                                 	{
			                                                 		throw new NotImplementedException();
			                                                 	}
			                                                 }).Cast<Argument>();

			continuation(others.Concat(convertedAsArgs), messages.Concat(messagesCreated.AsEnumerable()));
		}
	}
}
