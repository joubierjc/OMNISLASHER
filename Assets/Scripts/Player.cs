using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

	[SerializeField]
	private float speed;
	[SerializeField]
	private float fireCooldown;
	[SerializeField]
	private float maxRange;

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
		AcquireTarget();

		nextFire += Time.deltaTime;
		if (target && nextFire >= fireCooldown) {
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
		var go = PoolManager.Instance.GetObjectFrom("PlayerShot");
		go.transform.position = transform.position;
		go.transform.LookAt(new Vector3(
			target.position.x,
			go.transform.position.y,
			target.position.z
		));
		go.SetActive(true);
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
