using System;
using System.Collections.Generic;
using System.Linq;

using Saccharin;
using Saccharin.CommandLine;

namespace Examples
{

	internal sealed class Validated : Parser
	{
		internal Validated()
		{
			AddDescription(new PositionedArgumentDescription<string>(0) { MatchPattern = "^\\d+$"});
			AddDescription(new NamedArgumentDescription<bool>("boop") { IsFlag = true});
		}
	}
	internal class Program
	{
		private static void Main()
		{
			var commandLine = Parser.ParseDefault();
			var unfiltered = commandLine.Arguments.Count == 0;
			if (unfiltered || commandLine.Arguments.Flags("partial").All)
			{
				PartialApplication();
			}
			if (unfiltered || commandLine.Arguments.Flags("maybe").All)
			{
				MaybeProtection();
			}
			if (unfiltered || commandLine.Arguments.Flags("defensive").All)
			{
				TheBestOffense();
			}
			if (unfiltered || commandLine.Arguments.Flags("commandline").All)
			{
				CommandLineExamples();
			}
		}

		private static void CommandLineExamples()
		{
			Console.WriteLine("Command-line parsing is easy.");
			Console.WriteLine("For instance you executed this program with the following parameters: ");
			var result = Parser.ParseDefault();
			foreach (var positioned in result.Arguments.OfType<IPositioned>())
			{
				Console.WriteLine("Position {0}: {1} [{2}]", positioned.Position, positioned.StringValue, positioned.ValueType);
			}
			foreach (var named in result.Arguments.OfType<INamed>())
			{
				Console.WriteLine("{0}: {1} [{2}]", named.Name, named.StringValue, named.ValueType);
			}

			result = new Validated().Parse(Environment.GetCommandLineArgs().Skip(1).ToArray());
			foreach (var validationMessage in result.ValidationMessages)
			{
				Console.WriteLine(validationMessage);
			}
		}

		private static void TheBestOffense()
		{
			var nullHidingInHere = new[] {"a", "b", "c", null, "d",};
			try
			{
				IDoNotLikeNullsAtAll(nullHidingInHere);
			}
			catch (ArgumentNullException)
			{
				Console.WriteLine("See, he really doesn't like nulls.");
			}
		}

		private static void IDoNotLikeNullsAtAll(IEnumerable<string> nullHidingInHere)
		{
			nullHidingInHere.AllNotNull("nullHidingInHere");
		}

		private static void MaybeProtection()
		{
			Console.WriteLine("Guard against nulls and other error conditions.");
			Maybe.Null(() => (string)null, Console.WriteLine, () => Console.WriteLine("Received null value."));
			Maybe.Equal(1,
			            () => 10,
			            i => Console.WriteLine("I am protected from ones. See? {0}.", i),
			            () => Console.WriteLine("This shouldn't happen."));
			try
			{
				Console.WriteLine(Maybe.Null(() => (string)null));
			}
			catch (MethodReturnedNullException methodReturnedNullException)
			{
				Console.WriteLine("I got a null result.\n{0}", methodReturnedNullException);
			}

			try
			{
				Console.WriteLine(Maybe.Null(() => "Not a null returned here."));
			}
			catch (MethodReturnedNullException methodReturnedNullException)
			{
				Console.WriteLine("I got a null result.\n{0}", methodReturnedNullException);
			}
		}

		private static void PartialApplication()
		{
			Console.WriteLine("You can use partial application.");
			var capAtOneHundred = 100.Apply<int, int, int>(Math.Min);
			Console.WriteLine("=> 100.Apply<int, int, int>(Math.Min)");
			Console.WriteLine("In this case, types must be specified.");
			Enumerable.Range(90, 20)
				.ToList()
				.ForEach(i => Console.WriteLine("This should be <= 100: {0}", capAtOneHundred(i)));
		}
	}
}