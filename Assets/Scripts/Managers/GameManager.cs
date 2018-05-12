using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager> {

	public Boundary boundary;
	public Player CurrentPlayer;
	public int score;
	public int scoreMultiplier;
	public float intervaleBetweenSpawn;

	private Tween scoreTween;

	private void Start() {
		// Inits
		TimeManager.Instance.Init();
		PoolManager.Instance.Init();

		Init();
	}

	public void Init() {
		StartCoroutine(Round());
	}

	public void EndGame() {
		StopCoroutine(Round());
		var enemies = FindObjectsOfType<Enemy>();
		foreach (var item in enemies) {
			item.gameObject.SetActive(false);
		}
	}

	public void AddScore(int value) {
		scoreTween.Complete();
		scoreTween = DOTween.To(() => score,
			x => {
				score = x;
				HudManager.Instance.RefreshScoreText();
			},
			score + (value * scoreMultiplier),
			HudManager.Instance.transitionsDuration
		);
	}

	private IEnumerator Round() {
		while (true) {
			SpawnEnemy();
			yield return new WaitForSeconds(intervaleBetweenSpawn);
		}
	}

	private void SpawnEnemy() {
		GameObject go;
		if (Random.Range(0f, 1f) > 0.75f) {
			go = PoolManager.Instance.GetObjectFrom("RangedEnemy");
		}
		else {
			go = PoolManager.Instance.GetObjectFrom("CloseEnemy");
		}
		if (!go) {
			return;
		}
		go.transform.position = new Vector3(
			Random.Range(boundary.Xmin, boundary.Xmax),
			go.transform.position.y,
			Random.Range(boundary.Zmin, boundary.Zmax)
		);
		go.SetActive(true);
	}
}