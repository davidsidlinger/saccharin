using System.Linq;

namespace Saccharin.CommandLine
{
	///<summary>
	/// Used to check the state of multiple flags
	///</summary>
	public interface IFlagSet
	{
		///<summary>
		/// Gets whether all flags are set
		///</summary>
		bool All { get; }

		///<summary>
		/// Gets whether any flag is set
		///</summary>
		bool Any { get; }
	}
}
