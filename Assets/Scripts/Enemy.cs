using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MeshRenderer))]
public class Enemy : MonoBehaviour {

	[SerializeField]
	private float speed;
	[SerializeField]
	private TrailRenderer trail;

	private Transform target;
	private Rigidbody rb;
	private MeshRenderer mesh;
	private Health hp;

	private bool canPlay = false;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
		mesh = GetComponent<MeshRenderer>();
		hp = GetComponent<Health>();
	}

	private void Start() {
		target = GameManager.Instance.CurrentPlayer.transform;
	}

	private void FixedUpdate() {
		rb.MovePosition(Vector3.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime));
	}

	private void OnEnable() {
		hp.Value = 3;
		GameManager.Instance.CurrentEnemies.Add(this);
		if (trail) {
			trail.Clear();
		}
		StartCoroutine(Spawn());
	}

	private void OnDisable() {
		GameManager.Instance.CurrentEnemies.Remove(this);
		StopCoroutine(Spawn());
	}


	private IEnumerator Spawn() {

	}

}
