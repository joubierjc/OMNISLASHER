using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager> {

	public Boundary boundary;

	public Player CurrentPlayer { get; private set; }

	public List<Enemy> CurrentEnemies { get; private set; }

	protected override void Awake() {
		base.Awake();

		CurrentPlayer = FindObjectOfType<Player>();

		CurrentEnemies = new List<Enemy>();
		CurrentEnemies.AddRange(FindObjectsOfType<Enemy>());
	}

	public void StartSpawning() {
		StartCoroutine(Round());
	}

	private IEnumerator Round() {
		while (true) {
			SpawnEnemy();
			yield return new WaitForSeconds(1);
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
