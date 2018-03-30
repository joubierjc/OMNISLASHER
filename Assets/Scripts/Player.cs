using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

	[SerializeField]
	private float speed;
	[SerializeField]
	private float fireCooldown;
	[SerializeField]
	private float maxRange;
	[SerializeField]
	private GameObject weaponPrefab;

	private Transform target;

	private Boundary boundary;
	private Rigidbody rb;
	private new Transform transform;

	private float nextFire;
	private Weapon weapon;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
		transform = GetComponent<Transform>();
		InitWeapon();
	}

	private void Start() {
		target = null;
		nextFire = fireCooldown;
		boundary = GameManager.Instance.boundary;
	}

	private void Update() {
		AcquireTarget();

		nextFire += Time.deltaTime;
		if (target && nextFire >= fireCooldown && weapon) {
			weapon.Shoot();
			nextFire = 0;
		}
	}

	private void FixedUpdate() {
		var direction = new Vector3(
			Input.GetAxis("Horizontal"),
			0f,
			Input.GetAxis("Vertical")
		).normalized;

		rb.velocity = direction * speed;

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, boundary.Xmin, boundary.Xmax),
			transform.position.y,
			Mathf.Clamp(transform.position.z, boundary.Zmin, boundary.Zmax)
		);
	}

	private void InitWeapon() {
		if (weaponPrefab) {
			var go = Instantiate(weaponPrefab, transform.position, transform.rotation);
			go.transform.SetParent(transform);
			weapon = weaponPrefab.GetComponent<Weapon>();
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
}
