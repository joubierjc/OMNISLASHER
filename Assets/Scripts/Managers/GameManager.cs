using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager> {

	public Boundary boundary;

	public Player CurrentPlayer { get; private set; }

	public List<Enemy> CurrentEnemies { get; private set; }

	private void Start() {
		// Inits
		Init();
		TimeManager.Instance.Init();
		PoolManager.Instance.Init();

		//StartCoroutine(Round());
	}

	public void Init() {
		CurrentPlayer = FindObjectOfType<Player>();

		CurrentEnemies = new List<Enemy>();
		CurrentEnemies.AddRange(FindObjectsOfType<Enemy>());
	}

	private IEnumerator Round() {
		while (true) {
			SpawnEnemy();
			yield return new WaitForSeconds(2.5f);
		}
	}

	private void SpawnEnemy() {
		var go = PoolManager.Instance.GetObjectFrom("MediumEnemy");
		go.transform.position = new Vector3(
			Random.Range(boundary.Xmin, boundary.Xmax),
			go.transform.position.y,
			Random.Range(boundary.Zmin, boundary.Zmax)
		);
		go.SetActive(true);
	}
}
