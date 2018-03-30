using UnityEngine;

public class Weapon : MonoBehaviour {

	[SerializeField]
	private string objectPool;
	[SerializeField]
	private Transform[] shotSpawns;

	public void Shoot() {
		foreach (var item in shotSpawns) {
			var go = PoolManager.Instance.GetObjectFrom(objectPool);
			if (go) {
				// set world to local
				go.transform.position = item.position;
				go.transform.rotation = item.rotation;
				go.SetActive(true);
			}
		}
	}

}
