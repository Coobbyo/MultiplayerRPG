using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EnemySpawnerNetwork : NetworkBehaviour
{
    [SerializeField] private Transform enemyPrefab;
	private Transform enemyTransform;

    [SerializeField] private int enemyCap = 1;
    private int numEnemies = 0;

    private float spawnTimer = 0.5f;
    

    private void Update()
    {
        if(!IsServer) return;
        if(numEnemies >= enemyCap) return;
        if(spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
            return;
        }

        Debug.Log("Spawning Enemy");
        enemyTransform = Instantiate(enemyPrefab, transform);
		enemyTransform.GetComponent<NetworkObject>().Spawn(true);
        numEnemies++;

        spawnTimer = 0.5f + Random.Range(0.1f, 1f);
    }
}
