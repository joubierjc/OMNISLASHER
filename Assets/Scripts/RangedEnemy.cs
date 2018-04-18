using UnityEngine;

public class RangedEnemy : Enemy {

	[Header("Movement")]
	public float speed;
	public float fleeFactor;

	private void FixedUpdate() {
		rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);

		var fleeMultiplier = fleeFactor / Vector3.Distance(transform.position, target.position);
		rb.AddRelativeForce(Vector3.back * speed * fleeMultiplier, ForceMode.Impulse);

		var clamped = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0f, rb.velocity.z), speed);
		rb.velocity = new Vector3(clamped.x, rb.velocity.y, clamped.z);
	}
}