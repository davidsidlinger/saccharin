using System.Linq;

namespace Saccharin.CommandLine
{
	///<summary>
	/// Represents a pair of values
	///</summary>
	///<typeparam name="TFirst">The type of <see cref="Item1"/></typeparam>
	///<typeparam name="TSecond">The type of <see cref="Item2"/></typeparam>
	public class Pair<TFirst, TSecond>
	{
		private readonly TFirst _item1;
		private readonly TSecond _item2;

		///<summary>
		/// Creates an instance of <see cref="Pair{TFirst,TSecond}"/>
		///</summary>
		///<param name="item1">The value of <see cref="Item1"/></param>
		///<param name="item2">The value of <see cref="Item2"/></param>
		public Pair(TFirst item1, TSecond item2)
		{
			_item1 = item1;
			_item2 = item2;
		}

		///<summary>
		/// Gets the values of the first item
		///</summary>
		public TFirst Item1
		{
			get { return _item1; }
		}

		///<summary>
		/// Gets the value of the second item
		///</summary>
		public TSecond Item2
		{
			get { return _item2; }
		}
	}

	///<summary>
	/// Contains utility methods for creating and manipulating <see cref="Pair{TFirst,TSecond}"/> instances
	///</summary>
	public static class Pair
	{
		///<summary>
		/// Creates a <see cref="Pair{TFirst,TSecond}"/> from two items
		///</summary>
		///<param name="first">The first item in the pair</param>
		///<param name="second">The second item in the pair</param>
		///<typeparam name="TFirst">The type of <see cref="Pair{TFirst,TSecond}.Item1"/></typeparam>
		///<typeparam name="TSecond">The type of <see cref="Pair{TFirst,TSecond}.Item2"/></typeparam>
		///<returns>A <see cref="Pair{TFirst,TSecond}"/></returns>
		public static Pair<TFirst, TSecond> Create<TFirst, TSecond>(TFirst first, TSecond second)
		{
			return new Pair<TFirst, TSecond>(first, second);
		}

		///<summary>
		/// Creates a <see cref="Pair{TFirst,TSecond}"/> from a single item
		///</summary>
		///<param name="single">The value for both items in the <see cref="Pair{TFirst,TSecond}"/></param>
		///<typeparam name="TSingle">The type of <paramref name="single"/></typeparam>
		///<returns>A <see cref="Pair{TFirst,TSecond}"/></returns>
		public static Pair<TSingle, TSingle> AsPair<TSingle>(this TSingle single)
		{
			return new Pair<TSingle, TSingle>(single, single);
		}
	}
}
