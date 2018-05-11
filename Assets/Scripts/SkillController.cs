using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Player))]
public class SkillController : MonoBehaviour {

	[Header("Order skills from higher cost to lower cost")]
	public Skill[] skills;
	public Skill currentSkill;

	private Player player;

	private void Awake() {
		player = GetComponent<Player>();
		foreach (var item in skills) {
			item.holder = transform;
		}
		Init();
	}

	public void Init() {
		skills = skills.OrderByDescending(k => k.cost).ToArray();
	}

	private void Update() {
		RefreshCurrentSpecial();
		if (Input.GetKeyDown(KeyCode.Space)) {
			player.currentEnergy -= currentSkill.cost;
			StartCoroutine(currentSkill.CastCoroutine());
		}
	}

	private void RefreshCurrentSpecial() {
		for (int i = 0; i < skills.Length; i++) {
			if (player.currentEnergy >= skills[i].cost) {
				currentSkill = skills[i];
				break;
			}
		}
		HudManager.Instance.RefreshSpecialText(currentSkill.displayText);
	}
}
