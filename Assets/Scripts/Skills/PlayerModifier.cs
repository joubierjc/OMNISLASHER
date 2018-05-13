using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/New Player Modifier")]
public class PlayerModifier : Skill {

	public float health;
	public int score;

	public override IEnumerator CastCoroutine() {
		if(health == 0f && score == 0) {
			yield break;
		}
		if (!holder.gameObject.activeInHierarchy) {
			yield break;
		}
		var player = holder.GetComponent<Player>();
		if(!player) {
			yield break;
		}
		GameManager.Instance.regenSound.Play();
		var hp = player.GetComponent<Health>();
		hp.Value = Mathf.Clamp(hp.Value + health, 0f, player.maxHealth);
		GameManager.Instance.AddScore(score);
		HudManager.Instance.ChangeHealthDisplay(hp.Value);
		HudManager.Instance.RefreshHealthText(hp.Value);
		yield break;
	}
}
