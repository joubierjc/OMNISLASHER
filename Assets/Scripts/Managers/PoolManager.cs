using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : UnitySingleton<PoolManager> {

	[SerializeField]
	private ObjectPool[] _pools;

	private void Start() {
		foreach (var item in _pools) {
			item.Init();
		}
		GameManager.Instance.StartSpawning();
	}

	public GameObject GetObjectFrom(string poolIdentifier) {
		ObjectPool pool = null;
		foreach (var item in _pools) {
			if (item.identifier.Equals(poolIdentifier)) {
				pool = item;
				break;
			}
		}
		if (pool != null) {
			return pool.GetObject();
		}
		return null;
	}

}
