using DG.Tweening;
using UnityEngine;

public class TimeManager : UnitySingleton<TimeManager> {

	public float initialTimeScale = 1f;
	public float slowMotionTimeScale = 0.2f;
	public float slowMotionDuration = 2f;
	public float transitionDuration = 1f;
	public float resetTransitionDuration = 2f;
	public Ease ease = Ease.Linear;

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
		slowMotionTween = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, initialTimeScale, resetTransitionDuration)
			.SetEase(ease);
	}

}