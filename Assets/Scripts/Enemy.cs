using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour {

	[SerializeField]
	private float speed;
	[SerializeField]
	private TrailRenderer trail;
	[SerializeField]
	private MeshRenderer mesh;
	[SerializeField]
	private Collider coll;

	private Transform target;
	private Rigidbody rb;
	private Health hp;

	private bool canPlay = false;

	private WaitForSeconds meshBlink = new WaitForSeconds(.25f);

	private void Awake() {
		rb = GetComponent<Rigidbody>();
		hp = GetComponent<Health>();
	}

	private void Start() {
		target = GameManager.Instance.CurrentPlayer.transform;
	}

	private void FixedUpdate() {
		if (canPlay) {
			rb.MovePosition(Vector3.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime));
		}
	}

	private void OnEnable() {
		hp.Value = 3;
		GameManager.Instance.CurrentEnemies.Add(this);
		if (trail) {
			trail.Clear();
		}
		if (mesh && coll) {
			StartCoroutine(SpawnAnim());
		}
	}

	private void OnDisable() {
		GameManager.Instance.CurrentEnemies.Remove(this);
		if (mesh && coll) {
			StopCoroutine(SpawnAnim());
		}
		canPlay = false;
	}

	private IEnumerator SpawnAnim() {
		rb.isKinematic = true;
		coll.enabled = false;
		for (int i = 0; i < 2; i++) {
			mesh.enabled = false;
			yield return meshBlink;
			mesh.enabled = true;
			yield return meshBlink;
		}
		coll.enabled = true;
		rb.isKinematic = false;
		canPlay = true;
	}

}
