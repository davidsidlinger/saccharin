using System;
using System.Collections.Generic;
using System.Linq;

namespace Saccharin.CommandLine
{
	///<summary>
	/// Description of a command line argument
	///</summary>
	public abstract class ArgumentDescription
	{
		/// <summary>
		/// Initializes a new instance of the <see cref = "ArgumentDescription{TTarget}" /> class.
		/// </summary>
		protected ArgumentDescription()
		{
			IsRequired = false;
			MatchPattern = "";
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is required.
		/// </summary>
		public bool IsRequired { get; set; }

		/// <summary>
		/// Gets or sets the match pattern.
		/// </summary>
		public string MatchPattern { get; set; }

		///<summary>
		/// Rewrites the <see cref="Argument"/> enumeration, based on the description given.
		///</summary>
		///<param name="continuation">The <see cref="Action{T1, T2}"/> to invoke with the rewritten arguments and validation messages</param>
		///<param name="arguments">An <see cref="IEnumerable{T}"/> of <see cref="Argument"/> to be examined</param>
		///<param name="messages">A <see cref="ICollection{T}"/> of validation messages</param>
		public abstract void RewriteArguments(Action<IEnumerable<Argument>, IEnumerable<string>> continuation, IEnumerable<Argument> arguments, IEnumerable<string> messages);
	}

	///<summary>
	/// Description of a command line argument
	///</summary>
	public abstract class ArgumentDescription<TTarget> : ArgumentDescription
	{
		
	}
}
