using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawning : MonoBehaviour {
	public GameObject spawnContainer;
	public GameObject[] enemies;
	public int enemyLimit = 10;
	public float spawnTime = 3;

	private TankLifecycle _playerLifecycle;
	private List<Transform> _spawnPlanes = new List<Transform>();

	private void Awake() {
		_playerLifecycle = GameObject.FindGameObjectWithTag("Player").GetComponent<TankLifecycle>();

		foreach (Transform plane in spawnContainer.transform) {
			plane.GetComponent<MeshRenderer>().enabled = false;
			_spawnPlanes.Add(plane);
		}
	}

	void Start() {
		StartCoroutine(PlanSpawn());
	}

	private IEnumerator PlanSpawn() {
		while (true) {
			yield return new WaitForSeconds(spawnTime);

			if (_playerLifecycle.health <= 0) {
				yield break;
			}

			if (EnemyLifecycle.enemyCount < enemyLimit) {
				SpawnEnemy();
			}
		}
	}

	private void SpawnEnemy() {
		GameObject enemy = enemies[Random.Range(0, enemies.Length)];
		Transform plane = _spawnPlanes[Random.Range(0, _spawnPlanes.Count)];
		Vector3 spawnPoint = GetRandomPoint(plane);

		Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
		Pool.Get(enemy, spawnPoint, rot);
	}

	private Vector3 GetRandomPoint(Transform plane) {
		Quaternion rot = Quaternion.Euler(0, plane.rotation.eulerAngles.y, 0);
		float x = Random.Range(- 10 * plane.localScale.x / 2,
			10 * plane.localScale.x / 2);
		float z = Random.Range(- 10 * plane.localScale.z / 2,
			10 * plane.localScale.z / 2);
		// rotate the generated point as well as the plane
		Vector3 v = rot * new Vector3(x, 0, z);

		v.x += plane.position.x;
		v.z += plane.position.z;

		return v;
	}
}