using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

	[SerializeField]
	private float speed;
	[SerializeField]
	private float fireCooldown;

	private Transform target;

	private float nextFire;
	private Boundary boundary;
	private Rigidbody rb;
	private new Transform transform;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
		transform = GetComponent<Transform>();
	}

	private void Start() {
		target = null;
		nextFire = fireCooldown;
		boundary = GameManager.Instance.boundary;
	}

	private void Update() {
		LookAtNearestEnemy();

		nextFire += Time.deltaTime;
		if (nextFire >= fireCooldown) {
			Shot();
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

	private void Shot() {
		var shot = PoolManager.Instance.GetObjectFrom("PlayerShot");
		shot.transform.position = transform.position;
		shot.transform.rotation = transform.localRotation;
		shot.SetActive(true);
	}

	private void LookAtNearestEnemy() {
		Transform nearest = null;
		var minDist = Mathf.Infinity;
		foreach (var item in GameManager.Instance.CurrentEnemies) {
			var dist = Vector3.Distance(item.transform.position, transform.position);
			if (dist < minDist) {
				nearest = item.transform;
				minDist = dist;
			}
		}
		if (nearest) {
			transform.LookAt(nearest);
		}
	}
}
