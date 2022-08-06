namespace Bitwise.Core.Collections
{
	public interface IQueue<T> : IContainer<T>
	{
		void Push(T value);
		T Peek();
		T Pop();
	}
}