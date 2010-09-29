using System;
using System.Collections.Generic;
using System.Linq;

namespace Saccharin.CommandLine
{
	/// <summary>
	/// Contains extension methods
	/// </summary>
	public static class Extensions
	{
		///<summary>
		/// Zips a pair of <see cref="IEnumerable{T}"/>s, adding default values if <paramref name="partner"/> yields fewer items than <paramref name="source"/>
		///</summary>
		///<param name="source">The source</param>
		///<param name="partner">The partner</param>
		///<typeparam name="TSource">The type of items in <paramref name="source"/></typeparam>
		///<typeparam name="TPartner">The type of items in <paramref name="partner"/></typeparam>
		///<returns>
		/// An <see cref="IEnumerable{T}"/> of <see cref="Pair{TFirst,TSecond}"/>, where <see cref="Pair{TFirst,TSecond}.Item1"/> is an item 
		/// from <paramref name="source"/> and <see cref="Pair{TFirst,TSecond}.Item2"/> is an item from <paramref name="partner"/>
		/// </returns>
		public static IEnumerable<Pair<TSource, TPartner>> ZipWithDefault<TSource, TPartner>(this IEnumerable<TSource> source,
		                                                                                     IEnumerable<TPartner> partner)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (partner == null)
			{
				throw new ArgumentNullException("partner");
			}

			var sourceEnumerator = source.GetEnumerator();
			var partnerEnumerator = partner.GetEnumerator();

			while (sourceEnumerator.MoveNext())
			{
				var partnerMember = partnerEnumerator.MoveNext()
				                    	? partnerEnumerator.Current
				                    	: default(TPartner);
				yield return Pair.Create(sourceEnumerator.Current, partnerMember);
			}
		}
	}
}
