using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager> {

	public Boundary boundary;
	public Player CurrentPlayer;
	public int score;

	private Tween scoreTween;

	private void Start() {
		// Inits
		TimeManager.Instance.Init();
		PoolManager.Instance.Init();

		StartCoroutine(Round());
	}

	public void AddScore(int value) {
		scoreTween.Complete();
		scoreTween = DOTween.To(() => score,
			x => {
				score = x;
				HudManager.Instance.RefreshScoreText();
			},
			score + value,
			HudManager.Instance.transitionsDuration
		);
	}

	private IEnumerator Round() {
		while (true) {
			SpawnEnemy();
			yield return new WaitForSeconds(2.5f);
		}
	}

	private void SpawnEnemy() {
		var go = PoolManager.Instance.GetObjectFrom("CloseEnemy");
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