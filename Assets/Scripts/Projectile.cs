using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

	public float timeBeforeDisable;
	public float speed;
	public TrailRenderer trail;

	private new Transform transform;

	private void Awake() {
		transform = GetComponent<Transform>();
	}

	private void Update() {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	private void OnEnable() {
		if (trail) {
			trail.Clear();
		}
		StartCoroutine(DisableCoroutine());
	}

	private void OnCollisionEnter(Collision collision) {
		gameObject.SetActive(false);
	}

	private IEnumerator DisableCoroutine() {
		yield return new WaitForSeconds(timeBeforeDisable);
		gameObject.SetActive(false);
	}
}