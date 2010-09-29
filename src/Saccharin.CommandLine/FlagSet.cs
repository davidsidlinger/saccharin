using System;
using System.Collections.Generic;
using System.Linq;

namespace Saccharin.CommandLine
{
	internal class FlagSet : IFlagSet
	{
		private readonly IEnumerable<NamedArgument<bool>> _flags;

		internal FlagSet(IEnumerable<NamedArgument<bool>> flags)
		{
			if (flags == null || flags.Any(f => f == null))
			{
				throw new ArgumentNullException("flags");
			}
			_flags = flags;
		}

		#region IFlagSet Members

		public bool All
		{
			get { return _flags.All(f => f.Value); }
		}

		public bool Any
		{
			get { return _flags.Any(f => f.Value); }
		}

		#endregion
	}
}
