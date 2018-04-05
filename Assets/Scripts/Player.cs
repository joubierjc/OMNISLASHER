using UnityEngine;

[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(PlayerController))]
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

	private Transform target;

	private new Transform transform;
	private Weapon weapon;

	private float nextFire;

	private void Awake() {
		transform = GetComponent<Transform>();
		weapon = GetComponent<Weapon>();
	}

	private void Start() {
		target = null;
		nextFire = FireInterval;
	}

	//private void Update() {
	//	AcquireTarget();
	//	nextFire += Time.deltaTime;
	//	if (target && nextFire >= FireInterval && weapon) {
	//		transform.LookAt(new Vector3(
	//			target.position.x,
	//			transform.position.y,
	//			target.position.z
	//		));
	//		weapon.Shoot();
	//		nextFire = 0;
	//	}
	//}

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
