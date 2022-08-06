using System;

namespace Bitwise.Core.Collections
{
	[Flags]
	public enum FastListPreferences
	{
		None = 0,
		PreserveOrder = 1 << 0,
	}
}