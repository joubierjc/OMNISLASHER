using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : UnitySingleton<PoolManager> {

	[SerializeField]
	private List<ObjectPool> _pools = new List<ObjectPool>();

}
