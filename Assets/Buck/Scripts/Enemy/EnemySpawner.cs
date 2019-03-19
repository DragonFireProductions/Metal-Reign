//----------------------------------------------------
//Purpose: This is a bare bonse spawner for enemies
//To simply showcase for the first Alpha build
//----------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //How often enemies are spawned
    [SerializeField]
    float spawnRate;

    //Reference to the enemy
    [SerializeField]
    GameObject enemyPrefab;

    //Reference to the spawn location
    [SerializeField]
    Transform spawnPoint;

    //Spawn an enemy at this interval
    [SerializeField]
    float spawnTime;

	
	// Update is called once per frame
	void Update ()
    {
        SpawnEnemy();
	}

    void SpawnEnemy()
    {
        spawnTime += Time.deltaTime;

        if(spawnTime >= spawnRate)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            spawnTime = 0.0f;
        }
    }
}
