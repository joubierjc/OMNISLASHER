using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour {

	[SerializeField]
	private float speed;

	private Transform target;

	private Rigidbody rb;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	private void Start() {
		target = EntityManager.Instance.CurrentPlayer.transform;
	}

	private void FixedUpdate() {
		rb.MovePosition(Vector3.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime));
	}


}
