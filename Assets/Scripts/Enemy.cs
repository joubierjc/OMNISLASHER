using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour {

	[SerializeField]
	private float speed;
	[SerializeField]
	private TrailRenderer trail;

	private Transform target;
	private Rigidbody rb;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	private void Start() {
		target = GameManager.Instance.CurrentPlayer.transform;
	}

	private void FixedUpdate() {
		rb.MovePosition(Vector3.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime));
	}

	private void OnEnable() {
		GameManager.Instance.CurrentEnemies.Add(this);
		if (trail) {
			trail.Clear();
		}
	}

	private void OnDisable() {
		GameManager.Instance.CurrentEnemies.Remove(this);
	}

}
