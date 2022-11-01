using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EnemySpawnerNetwork : NetworkBehaviour
{
	[SerializeField] private Transform enemyPrefab;
	private Transform enemyTransform;

	[SerializeField] private int enemyCap = 10;
	private int numEnemies = 0;

	private float spawnTimer = 1f;
	

	private void Update()
	{
		if(!IsServer || !IsOwner) return;

		if(numEnemies >= enemyCap) return;
		if(spawnTimer > 0)
		{
			spawnTimer -= Time.deltaTime;
			return;
		}

		//Debug.Log("IsServer: " + IsServer);
		enemyTransform = Instantiate(enemyPrefab, transform);
		InitializeEnemy(enemyTransform);
		enemyTransform.GetComponent<NetworkObject>().Spawn(true);
		numEnemies++;

		spawnTimer += 0.1f + Random.Range(0.1f, 0.2f);
	}

	private void InitializeEnemy(Transform enemyTransfom)
	{
		Vector3 initialPosition = enemyTransform.localPosition;
		initialPosition.x += Random.Range(-10f, 10f);
		initialPosition.z += Random.Range(-10f, 10f);
		enemyTransform.localPosition = initialPosition;

		enemyTransform.localRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

		enemyTransform.localScale *= Random.Range(0.5f, 1f);

		enemyTransform.GetComponent<EnemyNetwork>().OnDespawn += OnEnemyDespawn;
	}

	private void OnEnemyDespawn()
	{
		numEnemies--;
	}
}
