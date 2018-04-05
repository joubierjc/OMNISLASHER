using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	[Header("Movement")]
	[SerializeField]
	private float speed;
	[SerializeField]
	private float decelerationFactor;

	[Header("Collision")]
	[SerializeField]
	private float bumpFactor;
	[SerializeField]
	private float disableDuration;
	private Tween collisionTween;

	private Rigidbody rb;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {
		var h = Input.GetAxis("Horizontal");
		var z = Input.GetAxis("Vertical");

		var direction = new Vector3(h, 0f, z);

		rb.AddForce(direction * speed, ForceMode.Impulse);

		var clamped = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0f, rb.velocity.z), speed);
		rb.velocity = new Vector3(clamped.x * decelerationFactor, rb.velocity.y, clamped.z * decelerationFactor);
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("Enemy")) {
			var hp = collision.gameObject.GetComponent<Health>();
			if (hp) {
				hp.Value--;
			}

			// changing deceleration factor to have a better impulse
			collisionTween.Complete(false);
			var df = decelerationFactor;
			decelerationFactor = decelerationFactor * 4;
			collisionTween = DOTween.To(() => decelerationFactor, x => decelerationFactor = x, df, disableDuration); //setting up the tween to reset the deceleration factor

			// screen shake
			Camera.main.GetComponent<ScreenShaker>().ShakeScreen(1f, 1f);

			// slow motion
			TimeManager.Instance.EnterSlowMotion();

			// add impulse
			rb.velocity = Vector3.zero;
			rb.AddForce(collision.contacts[0].normal.normalized * speed * bumpFactor, ForceMode.Impulse);
		}
	}

}
