using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CloseEnemy : MonoBehaviour {

	[Range(0, 1)]
	public float rotationFactor;
	public float speed;
	public float decelerationFactor;

	private Transform target;

	private new Transform transform;
	private Rigidbody rb;

	private void Awake() {
		transform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();
	}

	private void Start() {
		target = GameManager.Instance.CurrentPlayer.transform;
	}

	private void Update() {
		var targetRotation = Quaternion.LookRotation(target.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactor);
	}

	private void FixedUpdate() {
		rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);

		var clamped = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0f, rb.velocity.z), speed);
		rb.velocity = new Vector3(clamped.x, rb.velocity.y, clamped.z);
	}

}
