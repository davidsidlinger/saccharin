using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

using JetBrains.Annotations;

namespace Saccharin
{
	/// <summary>
	///   Extensions used to protect yourself.
	/// </summary>
	public static class Verify
	{
		/// <summary>
		///   Verifies that all items in <paramref name = "toVerify" /> are not null.
		/// </summary>
		/// <typeparam name = "T">The type of items in <paramref name = "toVerify" />.</typeparam>
		/// <param name = "toVerify">The <see cref = "IEnumerable{T}" /> to verify.</param>
		/// <param name = "argumentName">The name of an argument in the caller.</param>
		[AssertionMethod]
		public static void AllNotNull<T>(
			[NotNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] this IEnumerable<T> toVerify,
			[InvokerParameterName] [NotNull] string argumentName) where T : class
		{
			if (argumentName == null)
			{
				throw new ArgumentNullException("argumentName");
			}
			if (toVerify == null)
			{
				throw new ArgumentNullException(argumentName);
			}
			if (toVerify.Any(item => item == null))
			{
				throw new ArgumentNullException(argumentName,
				                                string.Format(CultureInfo.CurrentCulture, "An item in {0} is null.", argumentName));
			}
		}

		/// <summary>
		///   Verifies that all items in <paramref name = "argumentExpression" /> are not null.
		/// </summary>
		/// <typeparam name = "T">The type of items in <paramref name = "argumentExpression" />'s return value.</typeparam>
		/// <param name = "argumentExpression">An <see cref = "Expression{T}" /> that yields the parameter to be checked.</param>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static void AllNotNull<T>(Expression<Func<IEnumerable<T>>> argumentExpression) where T : class
		{
			if (argumentExpression == null)
			{
				throw new ArgumentNullException("argumentExpression");
			}
			var memberExpression = argumentExpression.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new ArgumentOutOfRangeException("argumentExpression", argumentExpression, "Expected a member expression.");
			}
			var argumentName = memberExpression.Member.Name;
			var actualEnumerable = argumentExpression.Compile()();
			actualEnumerable.AllNotNull(argumentName);
		}
	}
}