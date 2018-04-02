using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeManager : UnitySingleton<TimeManager> {

	[SerializeField]
	private float initialTimeScale = 1f;
	[SerializeField]
	private float slowMotionTimeScale = 0.2f;
	[SerializeField]
	private float slowMotionDuration = 2f;
	[SerializeField]
	private float transitionDuration = 1f;
	[SerializeField]
	private float resetTransitionDuration = 2f;
	[SerializeField]
	private Ease ease = Ease.Linear;

	private Tween slowMotionTween;

	public void Init() {
		Time.timeScale = initialTimeScale;
	}

	public void EnterSlowMotion() {
		CancelInvoke("ResetTimeScale");
		slowMotionTween.Kill();
		slowMotionTween = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, slowMotionTimeScale, transitionDuration)
			.SetEase(ease);
		Invoke("ResetTimeScale", slowMotionDuration);
	}

	public void ResetTimeScale() {
		slowMotionTween.Kill();
		slowMotionTween = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1f, resetTransitionDuration)
			.SetEase(ease);
	}

}