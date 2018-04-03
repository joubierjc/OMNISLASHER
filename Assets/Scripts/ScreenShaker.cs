using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Camera))]
public class ScreenShaker : MonoBehaviour {

	private Tween shakeTween;

	private Camera cam;

	private void Start() {
		cam = GetComponent<Camera>();
	}

	public void ShakeScreen(float duration, float strength = 3, int vibrato = 10, float randomness = 90, bool fadeOut = true) {
		shakeTween.Complete(false);
		shakeTween = cam.DOShakePosition(duration, strength, vibrato, randomness, fadeOut);
	}

}
