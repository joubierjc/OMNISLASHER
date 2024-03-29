﻿using UnityEngine;

public class Health : MonoBehaviour {
	[SerializeField]
	private float _value;
	public float Value {
		get {
			return _value;
		}
		set {
			_value = value;
			if (_value <= 0) {
				gameObject.SetActive(false);
			}
		}
	}
}