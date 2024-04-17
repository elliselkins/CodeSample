public class ObjectPool<T> where T : new()
{
	protected readonly T[] items;
	protected int max = 100000;
	protected int itemCount = 0;

	public int newCount = 0;
	public int reusedCount = 0;
	public int releasedCount = 0;

	public ObjectPool(int max = 100000)
	{
		this.max = max;
		items = new T[max];
	}

	public void ResetCounting()
	{
		newCount = 0;
		reusedCount = 0;
		releasedCount = 0;
	}

	public void Release(T item)
	{
		if (itemCount < max)
		{
			items[itemCount++] = item;
			releasedCount++;
		}
		else
			throw new System.Exception($"Trying to release beyond max count {max}");
	}

	public (T item, bool newItem) Get()
	{
		T item;
		if (itemCount > 0)
		{
			item = items[--itemCount];
			items[itemCount] = default;
			reusedCount++;
			return (item, false);
		}
		else
		{
			newCount++;
			return (new T(), true);
		}
	}
}