using NUnit.Framework;

namespace Saccharin.Fixtures
{
	[TestFixture]
	[Category("Defensive programming")]
	public class MaybeFixture
	{
		[Test]
		[Category("Fast")]
		public void EqualBranchesCorrectly()
		{
			Maybe.Equal(10, () => 10, i => Assert.Fail("Should not get here. It's guarded"), () => { });
			const int expected = 11;
			Maybe.Equal(10,
			            () => expected,
			            actual => Assert.That(actual, Is.EqualTo(expected)),
			            () => Assert.Fail("Should not get here."));
		}

		[Test]
		[Category("Fast")]
		public void EqualReturnsValueWhenNotEqual()
		{
			const int expected = 11;
			var actual = Maybe.Equal(10, () => expected);
			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		[Category("Fast")]
		[Category("Exception verification")]
		public void EqualThrowsExceptionWhenEqual()
		{
			Assert.That(
				() => { Maybe.Equal(10, () => 9 + 1); },
				Throws.TypeOf(typeof(MethodReturnedEqualException)));
		}

		[Test]
		[Category("Fast")]
		public void NullBranchesCorrectly()
		{
			Maybe.Null<string>(() => null, s => Assert.Fail("Should not get here. It's guarded"), () => { });
			const string expected = "Boop";
			Maybe.Null(() => expected,
			           actual => Assert.That(actual, Is.EqualTo(expected)),
			           () => Assert.Fail("Should not get here."));
		}

		[Test]
		[Category("Fast")]
		public void NullReturnsValueWhenNotNull()
		{
			var expected = new object();
			var actual = Maybe.Null(() => expected);
			Assert.That(actual, Is.SameAs(expected));
		}

		[Test]
		[Category("Fast")]
		[Category("Exception verification")]
		public void NullThrowsExceptionWhenNull()
		{
			Assert.That(
				() => { Maybe.Null(() => (string)null); },
				Throws.TypeOf(typeof(MethodReturnedNullException)));
		}
	}
}