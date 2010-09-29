using System;

using JetBrains.Annotations;

namespace Saccharin
{
	///<summary>
	///  Extensions for functional programming
	///</summary>
	public static class FunctionalExtensions
	{
		///<summary>
		///  Creates a partially-applied <see cref = "Action" />
		///</summary>
		///<param name = "applied">The parameter to be applied.</param>
		///<param name = "action">The action to partially-apply.</param>
		///<typeparam name = "TFirst">The type of the first parameter to <paramref name = "action" />.</typeparam>
		///<typeparam name = "TSecond">The type of the second parameter to <paramref name = "action" />.</typeparam>
		///<returns>An <see cref = "Action" /> with <paramref name = "applied" /> applied as the first parameter.</returns>
		///<exception cref = "ArgumentNullException"><paramref name = "action" /> is null.</exception>
		[NotNull]
		public static Action<TSecond> Apply<TFirst, TSecond>(this TFirst applied, [NotNull] Action<TFirst, TSecond> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return t2 => action(applied, t2);
		}

		///<summary>
		///  Creates a partially-applied <see cref = "Action" />
		///</summary>
		///<param name = "applied">The parameter to be applied.</param>
		///<param name = "action">The action to partially-apply.</param>
		///<typeparam name = "TFirst">The type of the first parameter to <paramref name = "action" />.</typeparam>
		///<typeparam name = "TSecond">The type of the second parameter to <paramref name = "action" />.</typeparam>
		///<typeparam name = "TThird">The type of the third parameter to <paramref name = "action" />.</typeparam>
		///<returns>An <see cref = "Action" /> with <paramref name = "applied" /> applied as the first parameter.</returns>
		///<exception cref = "ArgumentNullException"><paramref name = "action" /> is null.</exception>
		[NotNull]
		public static Action<TSecond, TThird> Apply<TFirst, TSecond, TThird>(this TFirst applied,
		                                                                     [NotNull] Action<TFirst, TSecond, TThird> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return (t2, t3) => action(applied, t2, t3);
		}

		///<summary>
		///  Creates a partially-applied <see cref = "Action" />
		///</summary>
		///<param name = "applied">The parameter to be applied.</param>
		///<param name = "action">The action to partially-apply.</param>
		///<typeparam name = "TFirst">The type of the first parameter to <paramref name = "action" />.</typeparam>
		///<typeparam name = "TSecond">The type of the second parameter to <paramref name = "action" />.</typeparam>
		///<typeparam name = "TThird">The type of the third parameter to <paramref name = "action" />.</typeparam>
		///<typeparam name = "TFourth">The type of the fourth parameter to <paramref name = "action" />.</typeparam>
		///<returns>An <see cref = "Action" /> with <paramref name = "applied" /> applied as the first parameter.</returns>
		///<exception cref = "ArgumentNullException"><paramref name = "action" /> is null.</exception>
		[NotNull]
		public static Action<TSecond, TThird, TFourth> Apply<TFirst, TSecond, TThird, TFourth>(this TFirst applied,
		                                                                                       [NotNull] Action
		                                                                                       	<TFirst, TSecond, TThird,
		                                                                                       	TFourth> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return (t2, t3, t4) => action(applied, t2, t3, t4);
		}

		///<summary>
		///  Creates a partially-applied <see cref = "Func{T1,TResult}" />
		///</summary>
		///<param name = "applied">The parameter to be applied.</param>
		///<param name = "func">The func to partially-apply.</param>
		///<typeparam name = "TFirst">The type of the first parameter to <paramref name = "func" />.</typeparam>
		///<typeparam name = "TSecond">The type of the second parameter to <paramref name = "func" />.</typeparam>
		///<typeparam name = "TResult">The type returned by <paramref name = "func" />.</typeparam>
		///<returns>An <see cref = "Func{T1,TResult}" /> with <paramref name = "applied" /> applied as the first parameter.</returns>
		///<exception cref = "ArgumentNullException"><paramref name = "func" /> is null.</exception>
		[NotNull]
		public static Func<TSecond, TResult> Apply<TFirst, TSecond, TResult>(this TFirst applied,
		                                                                     [NotNull] Func<TFirst, TSecond, TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			return t2 => func(applied, t2);
		}

		///<summary>
		///  Creates a partially-applied <see cref = "Func{T1,T2,TResult}" />
		///</summary>
		///<param name = "applied">The parameter to be applied.</param>
		///<param name = "func">The func to partially-apply.</param>
		///<typeparam name = "TFirst">The type of the first parameter to <paramref name = "func" />.</typeparam>
		///<typeparam name = "TSecond">The type of the second parameter to <paramref name = "func" />.</typeparam>
		///<typeparam name = "TThird">The type of the third parameter to <paramref name = "func" />.</typeparam>
		///<typeparam name = "TResult">The type returned by <paramref name = "func" />.</typeparam>
		///<returns>An <see cref = "Func{T1,T2,TResult}" /> with <paramref name = "applied" /> applied as the first parameter.</returns>
		///<exception cref = "ArgumentNullException"><paramref name = "func" /> is null.</exception>
		[NotNull]
		public static Func<TSecond, TThird, TResult> Apply<TFirst, TSecond, TThird, TResult>(this TFirst applied,
		                                                                                     [NotNull] Func
		                                                                                     	<TFirst, TSecond, TThird,
		                                                                                     	TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			return (t2, t3) => func(applied, t2, t3);
		}

		///<summary>
		///  Creates a partially-applied <see cref = "Func{T1,T2,T3,TResult}" />
		///</summary>
		///<param name = "applied">The parameter to be applied.</param>
		///<param name = "func">The func to partially-apply.</param>
		///<typeparam name = "TFirst">The type of the first parameter to <paramref name = "func" />.</typeparam>
		///<typeparam name = "TSecond">The type of the second parameter to <paramref name = "func" />.</typeparam>
		///<typeparam name = "TThird">The type of the third parameter to <paramref name = "func" />.</typeparam>
		///<typeparam name = "TFourth">The type of the fourth parameter to <paramref name = "func" />.</typeparam>
		///<typeparam name = "TResult">The type returned by <paramref name = "func" />.</typeparam>
		///<returns>An <see cref = "Func{T1,T2,T3,TResult}" /> with <paramref name = "applied" /> applied as the first parameter.</returns>
		///<exception cref = "ArgumentNullException"><paramref name = "func" /> is null.</exception>
		[NotNull]
		public static Func<TSecond, TThird, TFourth, TResult> Apply<TFirst, TSecond, TThird, TFourth, TResult>(
			this TFirst applied, [NotNull] Func<TFirst, TSecond, TThird, TFourth, TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			return (t2, t3, t4) => func(applied, t2, t3, t4);
		}
	}
}