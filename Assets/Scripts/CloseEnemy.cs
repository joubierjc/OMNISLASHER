using UnityEngine;

public class CloseEnemy : Enemy {

	[Header("Movement")]
	[Range(0, 1)]
	public float rotationFactor;
	public float speed;
	public float decelerationFactor;

	private Transform target;

	private void Start() {
		target = GameManager.Instance.CurrentPlayer.transform;
	}

	private void Update() {
		var modifiedTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
		var targetRotation = Quaternion.LookRotation(modifiedTarget - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactor);
	}

	private void FixedUpdate() {
		rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);

		var clamped = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0f, rb.velocity.z), speed);
		rb.velocity = new Vector3(clamped.x, rb.velocity.y, clamped.z);
	}
}