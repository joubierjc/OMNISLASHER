using UnityEngine;

public class Weapon : MonoBehaviour {

	[SerializeField]
	private string poolIdentifier;
	[SerializeField]
	private Transform[] shotSpawn;

	public void Shoot() {
		foreach (var item in shotSpawn) {
			if (!item.gameObject.activeInHierarchy) {
				continue;
			}
			var go = PoolManager.Instance.GetObjectFrom(poolIdentifier);
			if (go) {
				go.transform.position = item.position;
				go.transform.rotation = item.rotation;
				go.SetActive(true);
			}
		}
	}

}