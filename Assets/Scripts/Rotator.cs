using UnityEngine;

public class Rotator : MonoBehaviour {

	public Vector3 axis;
	public float angle;

	private new Transform transform;

	private void Awake() {
		transform = GetComponent<Transform>();
	}

	private void Start() {
		axis.Normalize();
	}

	private void Update() {
		transform.rotation *= Quaternion.AngleAxis(angle * Time.deltaTime, axis);
	}
}