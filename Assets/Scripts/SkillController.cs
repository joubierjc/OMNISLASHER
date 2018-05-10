using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Player))]
public class SkillController : MonoBehaviour {

	public Skill[] skills;

	private Player player;

	private void Awake() {
		player = GetComponent<Player>();
		foreach (var item in skills) {
			item.holder = transform;
		}
	}

	private void Update() {


		if (Input.GetKeyDown(KeyCode.Space)) {
			Slash azer = (Slash)skills[0];
			StartCoroutine(azer.CastCoroutine());
		}
	}

	private void RefreshCurrentSpecial() {

	}



}
