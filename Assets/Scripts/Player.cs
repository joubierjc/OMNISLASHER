using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Weapon))]
public class Player : MonoBehaviour {

	[Header("Combat")]
	[SerializeField]
	private float maxRange;
	[SerializeField]
	private float attackPerSecond;
	private float FireInterval {
		get {
			return attackPerSecond == 0f ? 0f : 1 / attackPerSecond;
		}
	}

	[Header("Movement")]
	[SerializeField]
	private float speed;

	[Header("Collision")]
	[SerializeField]
	private float bumpFactor;
	[SerializeField]
	private float disableDuration;

	private Transform target;
	private Boundary boundary;

	private Rigidbody rb;
	private new Transform transform;
	private Weapon weapon;

	private bool hasBeenTouched = false;

	private float nextFire;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
		transform = GetComponent<Transform>();
		weapon = GetComponent<Weapon>();
	}

	private void Start() {
		target = null;
		nextFire = FireInterval;
		boundary = GameManager.Instance.boundary;
	}

	private void Update() {
		if (hasBeenTouched) {
			return;
		}

		AcquireTarget();
		nextFire += Time.deltaTime;
		if (target && nextFire >= FireInterval && weapon) {
			transform.LookAt(new Vector3(
				target.position.x,
				transform.position.y,
				target.position.z
			));
			weapon.Shoot();
			nextFire = 0;
		}
	}

	private void FixedUpdate() {
		if (!hasBeenTouched) {
			var direction = new Vector3(
				Input.GetAxis("Horizontal"),
				0f,
				Input.GetAxis("Vertical")
			).normalized;

			rb.velocity = direction * speed;
		}

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, boundary.Xmin, boundary.Xmax),
			transform.position.y,
			Mathf.Clamp(transform.position.z, boundary.Zmin, boundary.Zmax)
		);
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("Enemy")) {
			CancelInvoke("ResetHasBeenTouched");
			hasBeenTouched = true;
			TimeManager.Instance.EnterSlowMotion();
			rb.AddForce(collision.contacts[0].normal.normalized * bumpFactor, ForceMode.Impulse);
			Invoke("ResetHasBeenTouched", disableDuration);
		}
	}

	private void AcquireTarget() {
		Transform nearest = null;
		var minDist = Mathf.Infinity;
		foreach (var item in GameManager.Instance.CurrentEnemies) {
			var dist = Vector3.Distance(item.transform.position, transform.position);
			if (dist < maxRange && dist < minDist) {
				nearest = item.transform;
				minDist = dist;
			}
		}
		target = nearest;
	}

	private void ResetHasBeenTouched() {
		hasBeenTouched = false;
	}
}
