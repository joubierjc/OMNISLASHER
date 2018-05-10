using System.Collections;
using UnityEngine;

public abstract class Skill : ScriptableObject {

	[HideInInspector]
	public Transform holder;

	public string hintText;
	public float cost;
}
