using System;

using JetBrains.Annotations;

namespace Saccharin
{
	///<summary>
	///  Guards against method return conditions
	///</summary>
	public static class Maybe
	{
		///<summary>
		///  Throws an exception if the result of <paramref name = "guarded" /> is equal to <paramref name = "guardAgainst" />; 
		///  otherwise, returns the result of <paramref name = "guarded" />.
		///</summary>
		///<param name = "guardAgainst">The <typeparamref name = "TResult" /> to prevent returning.</param>
		///<param name = "guarded">The <see cref = "Func{TResult}" /> to be guarded.</param>
		///<typeparam name = "TResult">The type returned by <paramref name = "guarded" />.</typeparam>
		///<returns>The result of invoking <paramref name = "guarded" />.</returns>
		///<exception cref = "MethodReturnException">The result of <paramref name = "guarded" /> is equal to <paramref name = "guardAgainst" />.</exception>
		///<exception cref = "ArgumentNullException"><paramref name = "guarded" /> is null.</exception>
		public static TResult Equal<TResult>(TResult guardAgainst, [NotNull] Func<TResult> guarded)
		{
			if (guarded == null)
			{
				throw new ArgumentNullException("guarded");
			}
			var result = guarded();
			if (Equals(guardAgainst, result))
			{
				throw new MethodReturnedEqualException(guardAgainst);
			}
			return result;
		}

		///<summary>
		///  Invokes one of two continuations, varying on the equality of <paramref name = "guarded" />'s return value
		///  with <paramref name = "guardAgainst" />.
		///</summary>
		///<param name = "guardAgainst">The <typeparamref name = "TResult" /> that determines which continuation to invoke.</param>
		///<param name = "guarded">The <see cref = "Func{TResult}" />, the result of which is compared and continued (or not).</param>
		///<param name = "passed">
		///  The <see cref = "Action{TResult}" /> that is invoked if <paramref name = "guarded" />'s return
		///  value is not equal to <paramref name = "guardAgainst" />.
		///</param>
		///<param name = "failed">
		///  The <see cref = "Action" /> that is invoked if <paramref name = "guarded" />'s return
		///  value is equal to <paramref name = "guardAgainst" />.
		///</param>
		///<typeparam name = "TResult">The return type of <paramref name = "guarded" />.</typeparam>
		///<exception cref = "ArgumentNullException"><paramref name = "guarded" /> is null.</exception>
		///<exception cref = "ArgumentNullException"><paramref name = "passed" /> is null.</exception>
		///<exception cref = "ArgumentNullException"><paramref name = "failed" /> is null.</exception>
		public static void Equal<TResult>(TResult guardAgainst,
		                                  [NotNull] Func<TResult> guarded,
		                                  [NotNull] Action<TResult> passed,
		                                  [NotNull] Action failed)
		{
			if (guarded == null)
			{
				throw new ArgumentNullException("guarded");
			}
			if (passed == null)
			{
				throw new ArgumentNullException("passed");
			}
			if (failed == null)
			{
				throw new ArgumentNullException("failed");
			}
			var result = guarded();
			if (Equals(guardAgainst, result))
			{
				failed();
			}
			else
			{
				passed(result);
			}
		}

		///<summary>
		///  Throws an exception if the result of <paramref name = "guarded" /> is null; 
		///  otherwise, returns the result of <paramref name = "guarded" />.
		///</summary>
		///<param name = "guarded">The <see cref = "Func{TResult}" /> to be guarded.</param>
		///<typeparam name = "TResult">The type returned by <paramref name = "guarded" />.</typeparam>
		///<returns>The result of invoking <paramref name = "guarded" />.</returns>
		///<exception cref = "MethodReturnedNullException">The result of <paramref name = "guarded" /> is null.</exception>
		///<exception cref = "ArgumentNullException"><paramref name = "guarded" /> is null.</exception>
		[NotNull]
		public static TResult Null<TResult>([NotNull] Func<TResult> guarded) where TResult : class
		{
			if (guarded == null)
			{
				throw new ArgumentNullException("guarded");
			}
			var result = guarded();
			if (result == null)
			{
				throw new MethodReturnedNullException();
			}
			return result;
		}

		///<summary>
		///  Invokes one of two continuations, varying on the null-ness of <paramref name = "guarded" />'s return value.
		///</summary>
		///<param name = "guarded">The <see cref = "Func{TResult}" />, the result of which is compared and continued (or not).</param>
		///<param name = "passed">
		///  The <see cref = "Action{TResult}" /> that is invoked if <paramref name = "guarded" />'s return
		///  value is not null.
		///</param>
		///<param name = "failed">
		///  The <see cref = "Action" /> that is invoked if <paramref name = "guarded" />'s return
		///  value is null.
		///</param>
		///<typeparam name = "TResult">The return type of <paramref name = "guarded" />.</typeparam>
		///<exception cref = "ArgumentNullException"><paramref name = "guarded" /> is null.</exception>
		///<exception cref = "ArgumentNullException"><paramref name = "passed" /> is null.</exception>
		///<exception cref = "ArgumentNullException"><paramref name = "failed" /> is null.</exception>
		public static void Null<TResult>(Func<TResult> guarded, Action<TResult> passed, Action failed)
			where TResult : class
		{
			if (guarded == null)
			{
				throw new ArgumentNullException("guarded");
			}
			if (passed == null)
			{
				throw new ArgumentNullException("passed");
			}
			if (failed == null)
			{
				throw new ArgumentNullException("failed");
			}
			var result = guarded();
			if (result == null)
			{
				failed();
			}
			else
			{
				passed(result);
			}
		}
	}
}