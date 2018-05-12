using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GameManager : FakeUnitySingleton<GameManager> {

	public Boundary boundary;
	public Player CurrentPlayer;
	public int score;
	public int scoreMultiplier;
	public float timeBeforeSpawn;
	public float intervaleBetweenSpawn;

	private bool isPlaying;

	private Tween scoreTween;

	private void Start() {
		// Inits
		TimeManager.Instance.Init();
		PoolManager.Instance.Init();

		Init();
	}

	public void Init() {
		isPlaying = true;
		StartCoroutine(Round());
	}

	public void EndGame() {
		isPlaying = false;
		var enemies = FindObjectsOfType<Enemy>();
		foreach (var item in enemies) {
			item.gameObject.SetActive(false);
		}
		HudManager.Instance.DisplayEndGame();
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
		yield return new WaitForSeconds(timeBeforeSpawn);
		while (isPlaying) {
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