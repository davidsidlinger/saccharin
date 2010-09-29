using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Saccharin.CommandLine
{
	/// <summary>
	///   Represents the results of command line parsing
	/// </summary>
	public class ParseResult
	{
		private readonly bool _isValid;
		private readonly List<string> _validationMessages = new List<string>();
		private readonly List<Argument> _arguments = new List<Argument>();

		/// <summary>
		///   Initializes a new instance of the <see cref = "ParseResult" /> class.
		/// </summary>
		/// <param name = "validationMessages">The validation messages.</param>
		/// <param name = "arguments">The arguments.</param>
		public ParseResult(IEnumerable<string> validationMessages, IEnumerable<Argument> arguments)
		{
			if (validationMessages == null || validationMessages.Any(m => m == null))
			{
				throw new ArgumentNullException("validationMessages");
			}
			if (arguments == null || arguments.Any(a => a == null))
			{
				throw new ArgumentNullException("arguments");
			}

			_validationMessages.AddRange(validationMessages);
			_arguments.AddRange(arguments);
			_isValid = _validationMessages.Count == 0;
		}

		/// <summary>
		///   Gets a value indicating whether this instance is valid.
		/// </summary>
		/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
		public bool IsValid
		{
			get { return _isValid; }
		}

		/// <summary>
		///   Gets the validation messages.
		/// </summary>
		/// <value>The validation messages.</value>
		public ReadOnlyCollection<string> ValidationMessages
		{
			get { return _validationMessages.AsReadOnly(); }
		}

		/// <summary>
		///   Gets the arguments.
		/// </summary>
		/// <value>The arguments.</value>
		public ReadOnlyCollection<Argument> Arguments
		{
			get { return _arguments.AsReadOnly(); }
		}
	}
}
