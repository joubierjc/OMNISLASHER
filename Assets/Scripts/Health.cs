using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	[SerializeField]
	private float _lifePoint;
	public float LifePoint {
		get {
			return _lifePoint;
		}
		set {
			_lifePoint = value;
			if (_lifePoint <= 0) {
				gameObject.SetActive(false);
			}
		}
	}
}
