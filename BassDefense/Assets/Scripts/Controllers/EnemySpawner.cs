using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public EnemyController enemyPrefab;
    public float[] spawnTimes;

    Transform playerBase;

    public void StartSpawning(float start, int wave, Transform pBase)
    {
        Debug.Log("Started Spawning");
        playerBase = pBase;
        InvokeRepeating("SpawnEnemy", start, spawnTimes[wave]);
    }

    public void StopSpawning()
    {
        Debug.Log("Stoped Spawning");
        CancelInvoke();
    }

    void SpawnEnemy()
    {
        EnemyController e = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as EnemyController;
        e.GoToTarget(playerBase.position);
    }
}
