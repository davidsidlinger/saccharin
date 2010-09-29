using System;
using System.Linq;

using NUnit.Framework;

namespace Saccharin.CommandLine.Fixtures
{
	[TestFixture]
	public class ParserFixture
	{
		[Test]
		[Category("Fast")]
		public void FlagsWithAndWithoutSign()
		{
			var strategy = new Parser();
			var actual = strategy.Parse("-a", "-b-", "-c+");
			Assert.That(actual.IsValid);

			Assert.That(actual.Arguments.Count, Is.EqualTo(3));
			var flagArguments = actual.Arguments.OfType<NamedArgument<bool>>();
			Assert.That(flagArguments.Count(), Is.EqualTo(3));
			flagArguments.Single(a => a.Name == "a" && a.Value);
			flagArguments.Single(a => a.Name == "b" && !a.Value);
			flagArguments.Single(a => a.Name == "c" && a.Value);
		}

		[Test]
		[Category("Fast")]
		public void PatternMatchingWorks()
		{
			var strategy = new Parser();
			strategy.AddDescription(new NamedArgumentDescription<string>("numbers") { MatchPattern = "^\\d+$" });
			var actual = strategy.Parse("--numbers=ab112d");
			Assert.That(actual.IsValid, Is.False);
			var actual2 = strategy.Parse("--numbers=12345");
			Assert.That(actual2.IsValid);
		}

		[Test]
		[Category("Fast")]
		public void PositionedArgumentValidation()
		{
			var strategy = new Parser();
			strategy.AddDescription(new PositionedArgumentDescription<string>(0) { MatchPattern = "^\\d+$" });
			var actual = strategy.Parse("ab112d", "111333442");
			Assert.That(actual.IsValid, Is.False);
			var actual2 = strategy.Parse("111333442", "ab112d");
			Assert.That(actual2.IsValid);
		}

		[Test]
		[Category("Fast")]
		public void PositionedArgumentsAfterANamedArgumentAreIgnored()
		{
			var strategy = new Parser();
			const string thisIsUnnamed = "This Is Unnamed";
			const string name = "named";
			const string namedValue = "namedvalue";
			var actual = strategy.Parse("a", "b", "c", "--" + name, namedValue, thisIsUnnamed);
			Assert.That(actual.IsValid);
			var positionOnlyArguments = actual.Arguments.OfType<PositionedArgument<string>>();
			Assert.That(positionOnlyArguments.Count(), Is.EqualTo(3));
			Assert.That(positionOnlyArguments.Any(a => a.Value == thisIsUnnamed), Is.False);
			Assert.That(actual.Arguments.OfType<NamedArgument<string>>().Any(a => a.Name == name && a.Value == namedValue));
		}

		[Test]
		[Category("Fast")]
		public void RequiredArgumentsWork()
		{
			var strategy = new Parser();
			strategy.AddDescription(new NamedArgumentDescription<string>("required") { IsRequired = true });
			var actual = strategy.Parse("--notrequired=mmmm");
			Assert.That(actual.IsValid, Is.False);
			var actual2 = strategy.Parse("--required=aaa");
			Assert.That(actual2.IsValid);
		}

		[Test]
		[Category("Fast")]
		public void SimplePositionalArguments()
		{
			var strategy = new Parser();
			var actual = strategy.Parse("a", "b", "c");
			Assert.That(actual.IsValid);
			Assert.That(actual.Arguments.Count, Is.EqualTo(3));
			var positionOnlyArguments = actual.Arguments.OfType<PositionedArgument<string>>();
			Assert.That(positionOnlyArguments.Count(), Is.EqualTo(3));
			positionOnlyArguments.Single(a => a.Value == "a" && a.Position == 0);
			positionOnlyArguments.Single(a => a.Value == "b" && a.Position == 1);
			positionOnlyArguments.Single(a => a.Value == "c" && a.Position == 2);
		}

		[Test]
		[Category("Fast")]
		public void StringArgumentsWithVariousSeparators()
		{
			var strategy = new Parser();
			var expected = new[]
			               {
			               	new { Name = "something", Value = "duh" },
			               	new { Name = "someother", Value = "wha?" },
			               	new { Name = "othern", Value = "boop" },
			               };

			var actual = strategy.Parse("--something=duh", "--someother:wha?", "-othern…boop");
			Assert.That(actual.Arguments.Count, Is.EqualTo(3));
			var stringArguments = actual.Arguments.OfType<NamedArgument<string>>();
			Assert.That(stringArguments.Count(), Is.EqualTo(3));
			var joined = from sa in stringArguments
			             join e in expected on new { sa.Name, sa.Value } equals new { e.Name, e.Value }
			             select sa;
			Assert.That(joined.Count(), Is.EqualTo(3));
		}

		[Test]
		[Category("Fast")]
		public void TreatsLooseUnnamedParameterAsValueForLastNamed()
		{
			var strategy = new Parser();
			const string someFilePath = "Some File Path";
			var actual = strategy.Parse("--file", someFilePath);
			Assert.That(actual.IsValid);
			Assert.That(actual.Arguments.Count, Is.EqualTo(1));
			var stringArgument = actual.Arguments.OfType<NamedArgument<string>>().Single(s => s.Name.Equals("file"));
			Assert.That(stringArgument.Value, Is.EqualTo(someFilePath));
		}

		[Test]
		[Category("Fast")]
		public void YouCanEvenHaveAValueThatLooksLikeAName()
		{
			var strategy = new Parser();
			var actual = strategy.Parse("--something=--thisisweird");
			Assert.That(actual.Arguments.Count, Is.EqualTo(1));
			var stringArgument = actual.Arguments.OfType<NamedArgument<string>>().Single(a => a.Name == "something");
			Assert.That(stringArgument.Value, Is.EqualTo("--thisisweird"));
		}

		[Test]
		[Category("Fast")]
		public void TypeConversion()
		{
			var parser = new Parser();
			parser.AddDescription(new NamedArgumentDescription<int>("number"));
			parser.AddDescription(new PositionedArgumentDescription<DateTime>(0));
			var actual = parser.Parse("2010-01-01", "--number=123", "--othern=123");
			Assert.That(actual.IsValid);
			var positionedDate = actual.Arguments.OfType<PositionedArgument<DateTime>>().SingleOrDefault();
			Assert.That(positionedDate, Is.Not.Null);
			Assert.That(positionedDate.Position, Is.EqualTo(0));
			Assert.That(positionedDate.Value, Is.EqualTo(new DateTime(2010, 1, 1)));
			var namedInt = actual.Arguments.OfType<NamedArgument<int>>().SingleOrDefault();
			Assert.That(namedInt, Is.Not.Null);
			Assert.That(namedInt.Name, Is.EqualTo("number"));
			Assert.That(namedInt.Value, Is.EqualTo(123));
			var strings = actual.Arguments.OfType<NamedArgument<string>>();
			Assert.That(strings.Count(), Is.EqualTo(1));
			Assert.That(strings.Single().Name, Is.EqualTo("othern"));
		}
	}
}
