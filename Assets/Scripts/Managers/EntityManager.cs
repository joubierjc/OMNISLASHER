using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : UnitySingleton<EntityManager> {



	public Player CurrentPlayer { get; private set; }

	public List<Enemy> CurrentEnemies { get; private set; }

	protected override void Awake() {
		base.Awake();

		CurrentPlayer = FindObjectOfType<Player>();

		CurrentEnemies = new List<Enemy>();
		CurrentEnemies.AddRange(FindObjectsOfType<Enemy>());
	}

}
