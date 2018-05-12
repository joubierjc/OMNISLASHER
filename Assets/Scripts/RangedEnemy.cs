using System.Collections;
using UnityEngine;

public class RangedEnemy : Enemy {

	[Header("Ranged Settings")]
	public string poolIdentifier;
	public float fleeFactor;
	public Transform shotSpawn;
	public float shotInterval;

	protected override void OnEnable() {
		base.OnEnable();
		StartCoroutine(ShotCoroutine());
	}

	protected override void OnDisable() {
		base.OnDisable();
		StopCoroutine(ShotCoroutine());
	}

	private void FixedUpdate() {
		rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);

		var fleeMultiplier = fleeFactor / Vector3.Distance(transform.position, target.position);
		rb.AddRelativeForce(Vector3.back * speed * fleeMultiplier, ForceMode.Impulse);

		var clamped = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0f, rb.velocity.z), speed);
		rb.velocity = new Vector3(clamped.x, rb.velocity.y, clamped.z);
	}

	private IEnumerator ShotCoroutine() {
		while (true) {
			yield return new WaitForSeconds(shotInterval);
			SpawnProjectile();
		}
	}

	private void SpawnProjectile() {
		var go = PoolManager.Instance.GetObjectFrom(poolIdentifier);
		if (!go) {
			return;
		}
		go.transform.position = shotSpawn.position;
		go.transform.rotation = shotSpawn.rotation;
		go.SetActive(true);
	}
}