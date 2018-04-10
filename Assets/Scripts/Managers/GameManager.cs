using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager> {

	public Boundary boundary;
	public Player CurrentPlayer;
	public int score;

	private void Start() {
		// Inits
		TimeManager.Instance.Init();
		PoolManager.Instance.Init();

		StartCoroutine(Round());
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