using System.Linq;

namespace Saccharin.CommandLine
{
	/// <summary>
	///   Represents a command-line argument that is an integer
	/// </summary>
	public class IntArgument : NamedArgument<int>
	{
		/// <summary>
		///   Initializes a new instance of the <see cref = "IntArgument" /> class.
		/// </summary>
		/// <param name = "name">The name.</param>
		/// <param name="isDoubleDashed"></param>
		/// <param name = "value">The value.</param>
		public IntArgument(string name, bool isDoubleDashed, int value) : base(name, isDoubleDashed, value) {}
	}
}
