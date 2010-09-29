using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace Saccharin.CommandLine
{
	///<summary>
	///  Responsible for extracting command-line arguments
	///</summary>
	public class Parser
	{
		private static readonly Regex _namedArgumentExpression =
			new Regex(@"^-(?<seconddash>-)?(?<name>\w+)(((?<flag>[+-])|(\W))(?<value>.+)?)?$");

		private readonly ICollection<ArgumentDescription> _rules = new List<ArgumentDescription>();

		///<summary>
		///  Gets the <see cref = "ArgumentDescription" />s to be used by the parser
		///</summary>
		public ReadOnlyCollection<ArgumentDescription> Descriptions
		{
			get { return _rules.ToList().AsReadOnly(); }
		}

		///<summary>
		/// Extracts <paramref name="arguments"/> with the default <see cref="Parser"/>
		///</summary>
		///<param name="arguments">The arguments to extract</param>
		///<returns>The result of extraction</returns>
		public static ParseResult ParseDefault(params string[] arguments)
		{
			return ParseDefault(arguments.AsEnumerable());
		}

		///<summary>
		/// Extracts <paramref name="arguments"/> with the default <see cref="Parser"/>
		///</summary>
		///<param name="arguments">The arguments to extract</param>
		///<returns>The result of extraction</returns>
		public static ParseResult ParseDefault(IEnumerable<string> arguments)
		{
			return new Parser().Parse(arguments);
		}

		///<summary>
		/// Extracts command-line parameters with the default <see cref="Parser"/>
		///</summary>
		///<returns>The result of extraction</returns>
		public static ParseResult ParseDefault()
		{
			return ParseDefault(Environment.GetCommandLineArgs().Skip(1).ToArray());
		}

		///<summary>
		///  Extracts arguments according to the hints given.
		///</summary>
		///<param name = "arguments">The arguments to be parsed</param>
		///<returns>The result of extraction</returns>
		///<exception cref = "ArgumentNullException"><paramref name = "arguments" /> or an element is null</exception>
		public virtual ParseResult Parse(params string[] arguments)
		{
			return Parse(arguments.AsEnumerable());
		}

		///<summary>
		///  Extracts arguments according to the hints given.
		///</summary>
		///<param name = "arguments">The arguments to be parsed</param>
		///<returns>The result of extraction</returns>
		///<exception cref = "ArgumentNullException"><paramref name = "arguments" /> or an element is null</exception>
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		public virtual ParseResult Parse(IEnumerable<string> arguments)
		{
			if (arguments == null || arguments.Any(a => a == null))
			{
				throw new ArgumentNullException("arguments");
			}

			var regexed = arguments
				.Select((a, i) => new { Value = a, Index = i, Match = _namedArgumentExpression.Match(a) })
				.ToList();

			var positionOnlies = regexed
				.TakeWhile(m => !m.Match.Success)
				.Select(m => new PositionedArgument<string>(m.Index, m.Value))
				.Cast<Argument>()
				.ToList();

			var mustBeNamed = regexed.SkipWhile(m => !m.Match.Success);
			var zipped = mustBeNamed
				.ZipWithDefault(mustBeNamed.Skip(1))
				.Select(p => new { Named = p.Item1, MaybeValue = p.Item2, })
				.Where(p => p.Named.Match.Success)
				.Select(p =>
				        {
				        	var isFlag = !p.Named.Match.Groups["value"].Success && (p.MaybeValue == null || p.MaybeValue.Match.Success);
				        	return
				        		new
				        		{
				        			p.Named,
				        			p.MaybeValue,
				        			IsFlag = isFlag,
				        			Name = p.Named.Match.Groups["name"].Value,
				        			DoubleDash = p.Named.Match.Groups["seconddash"].Success,
				        		};
				        })
				.ToList();

			var flags = zipped
				.Where(p => p.IsFlag)
				.Select(p =>
				        {
				        	var real = p.Named;
				        	var value =
				        		real.Match.Groups["flag"].Success
				        			? real.Match.Groups["flag"].Value.Equals("+", StringComparison.Ordinal)
				        			: true;
				        	return new NamedArgument<bool>(p.Name, p.DoubleDash, value);
				        })
				.Cast<Argument>();

			var withValues = zipped
				.Where(p => !p.IsFlag)
				.Select(p =>
				        {
				        	var valueGroup = p.Named.Match.Groups["value"];
				        	var value = valueGroup.Success ? valueGroup.Value : p.MaybeValue.Value;
				        	return new NamedArgument<string>(p.Name, p.DoubleDash, value);
				        })
				.Cast<Argument>();

			var parsed = positionOnlies.Concat(flags.Concat(withValues));

			var described = Describe(parsed);

			return new ParseResult(described.Item2, described.Item1);
		}

		///<summary>
		///  Validates a collection of <see cref = "Argument" />s
		///</summary>
		///<param name = "arguments">An <see cref = "ICollection{T}" /> of <see cref = "Argument" /></param>
		///<returns>A <see cref = "ICollection{T}" /> of validation messages</returns>
		public virtual Pair<IEnumerable<Argument>, IEnumerable<string>> Describe(IEnumerable<Argument> arguments)
		{
			IEnumerable<Argument> rewritten = null;
			IEnumerable<string> messages = null;

			Action<IEnumerable<Argument>, IEnumerable<string>> endpoint = (a, m) =>
			                                                              {
																																			rewritten = a;
			                                                              	messages = m;
			                                                              };

			var aggregated = Descriptions.Aggregate(
			                                        endpoint,
			                                        (acc, description) => (a, m) => description.RewriteArguments(acc, a, m));
			aggregated(arguments, new string[] { });
			Debug.Assert(messages != null);
			Debug.Assert(rewritten != null);
			return Pair.Create(rewritten, messages);
		}

		/// <summary>
		///   Adds the description.
		/// </summary>
		/// <param name = "description">The description.</param>
		public virtual void AddDescription(ArgumentDescription description)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			_rules.Add(description);
		}
	}
}
