using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

	private float speed;

	private Rigidbody rb;
	private TrailRenderer trail;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
		trail = GetComponent<TrailRenderer>();
	}

	private void OnEnable() {
		if (trail) {
			trail.Clear();
		}
	}

	private void OnTriggerEnter(Collider other) {

	}

}
