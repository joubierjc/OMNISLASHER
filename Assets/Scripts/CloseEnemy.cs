using UnityEngine;

public class CloseEnemy : Enemy {

	private void FixedUpdate() {
		rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);

		var clamped = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0f, rb.velocity.z), speed);
		rb.velocity = new Vector3(clamped.x, rb.velocity.y, clamped.z);
	}
}