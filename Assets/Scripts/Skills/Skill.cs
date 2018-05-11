using System.Collections;
using UnityEngine;

public abstract class Skill : ScriptableObject {

	[HideInInspector]
	public Transform holder;

	public string displayText;
	public float cost;

	public abstract IEnumerator CastCoroutine();
}
