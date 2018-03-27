using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

	[SerializeField]
	private float speed;
	[SerializeField]
	private TrailRenderer trail;

	private Rigidbody rb;
	private new Transform transform;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
		transform = GetComponent<Transform>();
	}

	private void Update() {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	private void OnEnable() {
		if (trail) {
			trail.Clear();
		}
	}

	private void OnTriggerEnter(Collider other) {
		gameObject.SetActive(false);
	}

}
