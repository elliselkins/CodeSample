using UnityEngine;

public class GameObjectPool : ObjectPool<GameObject>
{
	public GameObject prefab;

	public GameObjectPool (GameObject prefab = null, int max = 100000) : base (max)
	{
		this.prefab = prefab;
	}

	public (GameObject item, bool newItem) Get(Transform parentTransform = null)
	{
		GameObject item;
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
			return (GameObject.Instantiate(prefab, parentTransform), true);
		}
	}
}
