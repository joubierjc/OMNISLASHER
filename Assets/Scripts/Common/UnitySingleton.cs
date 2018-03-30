﻿using UnityEngine;

public class UnitySingleton<T> : MonoBehaviour where T : Component {

	public static T Instance { get; private set; }

	protected virtual void Awake() {
		if (Instance == null) {
			Instance = this as T;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}
}