using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float spawnTime;
    float nextSpawnTime;
    bool isSpawning = false;

    void Start() {
        StartSpawning();
    }


    void Update () {
        if(isSpawning) {
            if(nextSpawnTime <= 0) {
                nextSpawnTime = spawnTime;

                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
            else {
                nextSpawnTime -= Time.deltaTime;
            }
        }
	}

    public void StartSpawning() {
        Debug.Log("Started Spawning");
        isSpawning = true;
    }

    public void StopSpawning() {
        nextSpawnTime = spawnTime;
        isSpawning = false;
    }
}
