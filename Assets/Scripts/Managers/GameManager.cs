using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GameManager : UnityFakeSingleton<GameManager> {

	public Boundary boundary;
	public Player CurrentPlayer;
	public int score;
	public int scoreMultiplier;
	public float timeBeforeSpawn;
	public float intervaleBetweenSpawn;

	public AudioSource combatMusic;
	public AudioSource stopSound;
	public AudioSource slashSound;
	public AudioSource omnislashStartSound;
	public AudioSource shurikenStartSound;
	public AudioSource regenSound;
	public AudioSource hitSound;

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
		combatMusic.Play();
	}

	public void EndGame() {
		isPlaying = false;
		Cursor.visible = true;
		var enemies = FindObjectsOfType<Enemy>();
		foreach (var item in enemies) {
			item.gameObject.SetActive(false);
		}
		HudManager.Instance.DisplayEndGame();
		combatMusic.Stop();
		stopSound.Play();
	}

	public void AddScore(int value) {
		scoreTween.Complete();
		scoreTween = DOTween.To(() => score,
			x => {
				score = x;
				HudManager.Instance.RefreshScoreText();
				HudManager.Instance.RefreshEndScoreText();
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

	public void RandomSlashAudio() {
		slashSound.pitch = Random.Range(0.8f, 1f);
		slashSound.Play();
	}

	public void RandomHitAudio() {
		hitSound.pitch = Random.Range(0.8f, 1f);
		hitSound.Play();
	}
}