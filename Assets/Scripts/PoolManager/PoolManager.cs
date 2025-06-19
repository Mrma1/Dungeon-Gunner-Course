using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PoolManager : SingletonMonobehaviour<PoolManager>
{
    [SerializeField] private Pool[] poolArray = null;
    private Transform objectPoolTransform;
    private Dictionary<int, Queue<Component>> poolDictionary = new Dictionary<int, Queue<Component>>();

    [Serializable]
    public struct Pool
    {
        public int poolSize;
        public GameObject poolPrefab;
        public string componentType;
    }

	private void Start()
	{
        objectPoolTransform = gameObject.transform;

        for (int i = 0; i < poolArray.Length; i++)
        {
            CreatePool(poolArray[i].poolPrefab, poolArray[i].poolSize, poolArray[i].componentType);
        }
	}

    private void CreatePool(GameObject poolPrefab, int poolSize, string componentType)
    {
        int poolKey = poolPrefab.GetInstanceID();

        string prefabName = poolPrefab.name;

        GameObject parentGameObject = new GameObject(prefabName + "  Anchor");

        parentGameObject.transform.SetParent(objectPoolTransform);


		if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<Component>());

            for (int i = 0;i < poolSize; i++)
            {
                GameObject newObject = Instantiate(poolPrefab, parentGameObject.transform) as GameObject;

				newObject.SetActive(false);

				poolDictionary[poolKey].Enqueue(newObject.GetComponent(Type.GetType(componentType)));
            }
        }
    }

    public Component ReuseComponent(GameObject poolPrefab,  Vector3 position, Quaternion rotation)
    {
        int poolKey = poolPrefab.GetInstanceID();

        if(poolDictionary.ContainsKey(poolKey))
        {
            Component componentToReuse = GetComponentFromPool(poolKey);

            ResetObject(position, rotation, componentToReuse, poolPrefab);

            return componentToReuse;
        }
        else
        {
            Debug.Log("No object pool for " + poolPrefab);
            return null;
        }
    }

	private Component GetComponentFromPool(int poolKey)
    {
        Component componentToReuse = poolDictionary[poolKey].Dequeue();
        poolDictionary[poolKey].Enqueue(componentToReuse);

        if(componentToReuse.gameObject.activeSelf)
        {
            componentToReuse.gameObject.SetActive(false);
        }

        return componentToReuse;
    }

	private void ResetObject(Vector3 position, Quaternion rotation, Component componentToReuse, GameObject poolPrefab)
	{
        componentToReuse.transform.position = position;
        componentToReuse.transform.rotation = rotation;
        componentToReuse.gameObject.transform.localScale = poolPrefab.transform.localScale;
	}

	#region Validation
#if UNITY_EDITOR

	private void OnValidate()
	{
		HelperUtilities.ValidateCheckEnumerableValues(this, nameof(poolArray), poolArray);
	}

#endif
	#endregion
}
