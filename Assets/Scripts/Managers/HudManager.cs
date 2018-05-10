using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HudManager : UnitySingleton<HudManager> {

	[Header("Settings")]
	public float transitionsDuration;

	[Header("UI Elements")]
	public TextMeshProUGUI scoreText;
	public Slider healthBarSlider;
	public TextMeshProUGUI healthText;
	public Slider energyBarSlider;
	public TextMeshProUGUI energyText;

	private Tween scoreTween;
	private Tween healthBarTween;
	private Tween energyBarTween;

	public void ChangeHealthDisplay(float newValue) {
		healthBarTween.Kill();
		healthBarTween = DOTween.To(() => healthBarSlider.value, x => healthBarSlider.value = x, newValue, transitionsDuration);
		RefreshHealthText(newValue);
	}

	public void ChangeEnergyDisplay(float newValue) {
		energyBarTween.Kill();
		energyBarTween = DOTween.To(() => energyBarSlider.value, x => energyBarSlider.value = x, newValue, transitionsDuration);
		RefreshEnergyText(newValue);
	}

	public void RefreshScoreText() {
		scoreText.SetText("{0:0}", GameManager.Instance.score);
	}

	public void RefreshHealthText(float newValue) {
		healthText.SetText("{0:0}", newValue);
	}

	public void RefreshEnergyText(float newValue) {
		energyText.SetText("{0:0}", newValue);
	}
}
