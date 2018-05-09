using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Damager))]
public class Player : MonoBehaviour {

	[Header("Combat")]
	public float maxHealth;

	[Header("Movement")]
	public float speed;
	public float decelerationFactor;

	[Header("Collision")]
	public float bumpFactor;
	public float disableDuration;
	public float decelerationFactorMultiplier;
	private Tween collisionTween;

	private Rigidbody rb;
	private Health hp;
	private Damager dmg;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
		hp = GetComponent<Health>();
		dmg = GetComponent<Damager>();
	}

	private void FixedUpdate() {
		var h = Input.GetAxis("Horizontal");
		var z = Input.GetAxis("Vertical");

		var direction = new Vector3(h, 0f, z);

		rb.AddForce(direction * speed, ForceMode.Impulse);

		var clamped = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0f, rb.velocity.z), speed);
		rb.velocity = new Vector3(clamped.x * decelerationFactor, rb.velocity.y, clamped.z * decelerationFactor);
	}

	private void OnEnable() {
		hp.Value = maxHealth;
	}

	private void OnCollisionEnter(Collision collision) {
		var health = collision.collider.GetComponent<Health>();
		var damager = collision.collider.GetComponent<Damager>();

		if (health || damager) {
			// changing deceleration factor to have a better impulse
			collisionTween.Complete(false);
			var df = decelerationFactor;
			decelerationFactor = decelerationFactor * decelerationFactorMultiplier;
			collisionTween = DOTween.To(() => decelerationFactor, x => decelerationFactor = x, df, disableDuration); //setting up the tween to reset the deceleration factor

			// screen shake
			Camera.main.GetComponent<ScreenShaker>().ShakeScreen(1f, 1f);

			// slow motion
			TimeManager.Instance.EnterSlowMotion();

			// add impulse
			rb.velocity = Vector3.zero;
			rb.AddForce(collision.contacts[0].normal.normalized * speed * bumpFactor, ForceMode.Impulse);
		}

		if (damager) {
			hp.Value -= damager.value;
		}

		if (health) {
			health.Value -= dmg.value;
		}
	}
}