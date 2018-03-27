using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Object Pool/New Object Pool")]
public class ObjectPool : ScriptableObject {

	public string identifier;
	public GameObject template;
	public int initialSize;
	public bool canGrow;

	private List<GameObject> pooledObjects;

	public void Init() {
		pooledObjects = new List<GameObject>();
		for (int i = 0; i < initialSize; i++) {
			var go = Instantiate(template);
			go.SetActive(false);
			pooledObjects.Add(go);
		}
	}

	public GameObject GetObject() {
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects[i].activeInHierarchy) {
				return pooledObjects[i];
			}
		}

		if (canGrow) {
			var go = Instantiate(template);
			pooledObjects.Add(go);
			return go;
		}

		return null;
	}

}
