using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Transform playerBase;
    Wave currentWave;
    WaveController waveController;

    void Start() {
        waveController = GameObject.Find("Game Controller").GetComponent<WaveController>();
    }

    public void StartSpawning(float start, Wave cWave, Transform pBase)
    {
        Debug.Log("Started Spawning");
        playerBase = pBase;
        currentWave = cWave;

        foreach(EnemyGroup enemyGroup in currentWave.enemies) {
            StartCoroutine(SpawnEnemy(enemyGroup.enemyPrefab, enemyGroup.numEnemy, start, enemyGroup.timeBetweenEnemies));
        } 
    }

    public void StopSpawning()
    {
        Debug.Log("Stoped Spawning");
    }

    IEnumerator SpawnEnemy(EnemyController enemyPrefab, int numInWave, float start, float repeat)
    {
        yield return new WaitForSeconds(start);

        int numSpawned = 0;

        while(numSpawned <= numInWave) {
            EnemyController e = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as EnemyController;
            e.GoToTarget(playerBase.position);
            e.onDeath.AddListener(waveController.UpdateNumKilled);
            yield return new WaitForSeconds(repeat);
        }
    }
}
