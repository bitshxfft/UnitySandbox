namespace Bitwise.Core.Collections
{
	public interface IContainer<T>
	{
		public int Count { get; }
		public int Capacity { get; }
		
		// #SD: TODO
	}
}