using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/New Player Shot Mode")]
public class PlayerShotMode : Skill {

	public string poolIdentifier;
	public int numberOfShots;
	public float radius;
	public float timeBetweenShots;
	public LayerMask layer;

	public override IEnumerator CastCoroutine() {
		if (!holder.gameObject.activeInHierarchy) {
			yield break;
		}
		List<Enemy> enemies = new List<Enemy>();
		foreach (var item in Physics.OverlapSphere(holder.position, radius, layer.value)) {
			var enemy = item.GetComponent<Enemy>();
			if (enemy) {
				enemies.Add(enemy);
			}
		}
		GameManager.Instance.shurikenStartSound.Play();
		for (int i = 0; i < enemies.Count; i++) {
			if (i > numberOfShots - 1) {
				break;
			}
			var go = PoolManager.Instance.GetObjectFrom(poolIdentifier);
			if (go) {
				go.transform.position = holder.position;
				var lookPos = new Vector3(enemies[i].transform.position.x, holder.position.y, enemies[i].transform.position.z);
				go.transform.LookAt(lookPos);
				go.SetActive(true);
			}
			yield return new WaitForSeconds(timeBetweenShots);
		}
	}
}
