using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Skill/New Slash")]
public class Slash : Skill {
	public float radius;
	public float timeBetweenStrikes;
	public LayerMask layer;
	public Ease ease;

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
		var collider = holder.gameObject.GetComponent<Collider>();
		if (collider) {
			collider.enabled = false;
		}
		foreach (var item in enemies) {
			holder.DOMove(
				new Vector3(item.gameObject.transform.position.x, holder.position.y, item.gameObject.transform.position.z),
				timeBetweenStrikes
			).SetEase(ease);
			var hp = item.GetComponent<Health>();
			if (hp) {
				hp.Value = 0f;
			}
			item.gameObject.SetActive(false);
			yield return new WaitForSeconds(timeBetweenStrikes);
		}
		if (collider) {
			collider.enabled = true;
		}
		TimeManager.Instance.EnterSlowMotion();
	}
}
