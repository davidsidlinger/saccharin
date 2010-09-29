using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace Saccharin.Fixtures
{
	[TestFixture]
	[Category("Defensive programming")]
	public class VerifyFixture
	{
		[Test]
		[Category("Fast")]
		public void AllNotNullThrowsWhenEnumerableIsNull()
		{
			const string argumentName = "isNull";
			Assert.That(
				// ReSharper disable AssignNullToNotNullAttribute
				() => ((IEnumerable<string>)null).AllNotNull(argumentName),
				// ReSharper restore AssignNullToNotNullAttribute
				Throws.TypeOf(typeof(ArgumentNullException)).With.Property("ParamName").EqualTo(argumentName));
		}

		[Test]
		[Category("Fast")]
		public void AllNotNullThrowsWhenGivenNullArgumentName()
		{
			var strings = new string[] {};
			const string argumentName = null;
			Assert.That(
				// ReSharper disable AssignNullToNotNullAttribute
				() => strings.AllNotNull(argumentName),
				// ReSharper restore AssignNullToNotNullAttribute
				Throws.TypeOf(typeof(ArgumentNullException)).With.Property("ParamName").EqualTo("argumentName"));
		}

		[Test]
		[Category("Fast")]
		public void AllNotNullThrowsWhenItemIsNull()
		{
			const string argumentName = "hasNullItem";
			var strings = new[] {"Not null", null,};
			Assert.That(
				// ReSharper disable AssignNullToNotNullAttribute
				() => strings.AllNotNull(argumentName),
				// ReSharper restore AssignNullToNotNullAttribute
				Throws.TypeOf(typeof(ArgumentNullException)).With.Property("ParamName").EqualTo(argumentName));
		}

		[Test]
		[Category("Fast")]
		public void ExpressionBasedFindsTheName()
		{
			var strings = new[] {"Not null", null,};
			Assert.That(
				// ReSharper disable AssignNullToNotNullAttribute
				() => Verify.AllNotNull(() => strings),
				// ReSharper restore AssignNullToNotNullAttribute
				Throws.TypeOf(typeof(ArgumentNullException)).With.Property("ParamName").EqualTo("strings"));
		}
	}
}