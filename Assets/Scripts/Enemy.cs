using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour {

	public float maxHealth;

	[Header("Aesthetic (Optionnal)")]
	public MeshRenderer[] meshRenderers;
	public Collider[] colliders;
	public TrailRenderer[] trails;

	protected new Transform transform;
	protected Rigidbody rb;
	protected Health hp;

	protected virtual void Awake() {
		transform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();
		hp = GetComponent<Health>();
	}

	protected virtual void OnEnable() {
		hp.Value = maxHealth;
		foreach (var item in trails) {
			item.Clear();
		}
		if (meshRenderers.Any() && colliders.Any()) {
			StartCoroutine(SpawnAnim());
		}
	}

	protected virtual void OnDisable() {
		if (meshRenderers.Any() && colliders.Any()) {
			StopCoroutine(SpawnAnim());
		}
	}

	protected virtual IEnumerator SpawnAnim() {
		rb.isKinematic = true;
		foreach (var item in colliders) {
			item.enabled = false;
		}
		for (int i = 0; i < 2; i++) {
			foreach (var item in meshRenderers) {
				item.enabled = true;
			}
			yield return new WaitForSeconds(.25f);
			foreach (var item in meshRenderers) {
				item.enabled = false;
			}
			yield return new WaitForSeconds(.25f);
		}
		foreach (var item in meshRenderers) {
			item.enabled = true;
		}
		foreach (var item in colliders) {
			item.enabled = true;
		}
		rb.isKinematic = false;
	}
}