using System;
using System.Collections.Generic;
using System.Linq;

namespace Saccharin.CommandLine
{
	///<summary>
	/// Contains extension methods to be made public
	///</summary>
	public static class PublicExtensions
	{
		///<summary>
		/// Finds all named arguments that represent a value of <typeparamref name="TArgument"/> type with the given name
		///</summary>
		///<param name="source">The <see cref="IEnumerable{T}"/> of arguments to be searched</param>
		///<param name="name">The name to search for</param>
		///<typeparam name="TArgument">The type of the argument value</typeparam>
		///<returns>An <see cref="IEnumerable{T}"/> of named arguments matching <paramref name="name"/></returns>
		public static IEnumerable<NamedArgument<TArgument>> FindAllByName<TArgument>(this IEnumerable<Argument> source,
		                                                                             string name)
		{
			return source.FindAllByName<TArgument>(name, StringComparison.OrdinalIgnoreCase);
		}

		///<summary>
		/// Finds all named arguments that represent a value of <typeparamref name="TArgument"/> type with the given name
		///</summary>
		///<param name="source">The <see cref="IEnumerable{T}"/> of arguments to be searched</param>
		///<param name="name">The name to search for</param>
		///<param name="stringComparison">The <see cref="StringComparison"/> to use for the search</param>
		///<typeparam name="TArgument">The type of the argument value</typeparam>
		///<returns>An <see cref="IEnumerable{T}"/> of named arguments matching <paramref name="name"/></returns>
		public static IEnumerable<NamedArgument<TArgument>> FindAllByName<TArgument>(this IEnumerable<Argument> source,
		                                                                             string name,
		                                                                             StringComparison stringComparison)
		{
			return source.FindAllByName(name, stringComparison).OfType<NamedArgument<TArgument>>();
		}

		///<summary>
		/// Finds all named arguments with the given name
		///</summary>
		///<param name="source">The <see cref="IEnumerable{T}"/> of arguments to be searched</param>
		///<param name="name">The name to search for</param>
		///<returns>An <see cref="IEnumerable{T}"/> of named arguments matching <paramref name="name"/></returns>
		public static IEnumerable<INamed> FindAllByName(this IEnumerable<Argument> source, string name)
		{
			return FindAllByName(source, name, StringComparison.OrdinalIgnoreCase);
		}

		///<summary>
		/// Finds all named arguments with the given name
		///</summary>
		///<param name="source">The <see cref="IEnumerable{T}"/> of arguments to be searched</param>
		///<param name="name">The name to search for</param>
		///<param name="stringComparison">The <see cref="StringComparison"/> to use for the search</param>
		///<returns>An <see cref="IEnumerable{T}"/> of named arguments matching <paramref name="name"/></returns>
		public static IEnumerable<INamed> FindAllByName(this IEnumerable<Argument> source,
		                                                string name,
		                                                StringComparison stringComparison)
		{
			if (source == null || source.Any(a => a == null))
			{
				throw new ArgumentNullException("source");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentOutOfRangeException("name", name, "Name cannot be null or empty.");
			}
			return source.OfType<INamed>().Where(a => a.Name.Equals(name, stringComparison));
		}

		///<summary>
		/// Finds a single named argument that represent a value of <typeparamref name="TArgument"/> type with the given name
		///</summary>
		///<param name="source">The <see cref="IEnumerable{T}"/> of arguments to be searched</param>
		///<param name="name">The name to search for</param>
		///<typeparam name="TArgument">The type of the argument value</typeparam>
		///<returns>The named argument matching <paramref name="name"/></returns>
		public static NamedArgument<TArgument> SingleByName<TArgument>(this IEnumerable<Argument> source, string name)
		{
			return source.SingleByName<TArgument>(name, StringComparison.OrdinalIgnoreCase);
		}

		///<summary>
		/// Finds a single named argument that represent a value of <typeparamref name="TArgument"/> type with the given name
		///</summary>
		///<param name="source">The <see cref="IEnumerable{T}"/> of arguments to be searched</param>
		///<param name="name">The name to search for</param>
		///<param name="stringComparison">The <see cref="StringComparison"/> to use for the search</param>
		///<typeparam name="TArgument">The type of the argument value</typeparam>
		///<returns>The named argument matching <paramref name="name"/></returns>
		public static NamedArgument<TArgument> SingleByName<TArgument>(this IEnumerable<Argument> source,
		                                                               string name,
		                                                               StringComparison stringComparison)
		{
			if (source == null || source.Any(a => a == null))
			{
				throw new ArgumentNullException("source");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentOutOfRangeException("name", name, "Name cannot be null or empty.");
			}
			return source.OfType<NamedArgument<TArgument>>().SingleOrDefault(a => a.Name.Equals(name, stringComparison));
		}

		///<summary>
		/// Finds a single named argument with the given name
		///</summary>
		///<param name="source">The <see cref="IEnumerable{T}"/> of arguments to be searched</param>
		///<param name="name">The name to search for</param>
		///<returns>An <see cref="INamed"/> matching <paramref name="name"/></returns>
		public static INamed SingleByName(this IEnumerable<Argument> source, string name)
		{
			return source.SingleByName(name, StringComparison.OrdinalIgnoreCase);
		}

		///<summary>
		/// Finds a single named argument with the given name
		///</summary>
		///<param name="source">The <see cref="IEnumerable{T}"/> of arguments to be searched</param>
		///<param name="name">The name to search for</param>
		///<param name="stringComparison">The <see cref="StringComparison"/> to use for the search</param>
		///<returns>An <see cref="INamed"/> matching <paramref name="name"/></returns>
		public static INamed SingleByName(this IEnumerable<Argument> source, string name, StringComparison stringComparison)
		{
			if (source == null || source.Any(a => a == null))
			{
				throw new ArgumentNullException("source");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentOutOfRangeException("name", name, "Name cannot be null or empty.");
			}
			return source.OfType<INamed>().SingleOrDefault(a => a.Name.Equals(name, stringComparison));
		}

		///<summary>
		/// Gets flags with a given name
		///</summary>
		///<param name="source">The <see cref="IEnumerable{T}"/> of arguments to search</param>
		///<param name="name">The name of the flags</param>
		///<param name="stringComparison">The <see cref="StringComparison"/> to use</param>
		///<returns>An <see cref="IFlagSet"/> representing the flags</returns>
		public static IFlagSet Flags(this IEnumerable<Argument> source, string name, StringComparison stringComparison)
		{
			return new FlagSet(source.FindAllByName<bool>(name, stringComparison));
		}

		///<summary>
		/// Gets flags with a given name
		///</summary>
		///<param name="source">The <see cref="IEnumerable{T}"/> of arguments to search</param>
		///<param name="name">The name of the flags</param>
		///<returns>An <see cref="IFlagSet"/> representing the flags</returns>
		public static IFlagSet Flags(this IEnumerable<Argument> source, string name)
		{
			return new FlagSet(source.FindAllByName<bool>(name));
		}
	}
}
