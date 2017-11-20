using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StartEvent : UnityEvent<float, Wave, Transform> { }

[System.Serializable]
public class EnemyGroup
{
    public int numEnemy;
    public float timeBetweenEnemies;
    public EnemyController enemyPrefab;
}

[System.Serializable]
public class Wave
{
    public EnemyGroup[] enemies;
}

public class WaveController : MonoBehaviour
{
    public float timeBeforeNextWave;

    //Events
    public StartEvent onWaveStart;
    public UnityEvent onWaveEnd;

    public Wave[] waves;
    int waveIdx;
    int numWaves;
    int numEnemies;
    int numspawners;
    public static int numKilled;

    UIController uiController;
    Transform home;

    bool gameEnd;

    public void Initialize(Transform _home, List<EnemySpawner> spawners)
    {
        uiController = GetComponent<UIController>();
        home = _home;
        numspawners = spawners.Count;
        foreach (EnemySpawner spawner in spawners)
        {
            onWaveStart.AddListener(spawner.StartSpawning);
            onWaveEnd.AddListener(spawner.StopSpawning);
        }

        numWaves = waves.Length;

        waveIdx = -1;
        gameEnd = false;
    }

    public void StartWaves()
    {
        waveIdx = 0;
        numEnemies = GetNumEnemies(waves[0]);
        numKilled = 0;
        onWaveStart.Invoke(timeBeforeNextWave, waves[waveIdx], home);
        Invoke("UpdateWave", timeBeforeNextWave);
    }


    void Update()
    {
        if (waveIdx > -1 && !gameEnd)
        {
            //print("killed: " + numKilled);
            //print("enemies: " + numEnemies);
            //print("wave: " + (waveIdx + 1));
            //print("waves: " + numWaves);

            if (BaseController.hp <= 0 || PlayerController.health <= 0)
            {
                gameEnd = true;
                uiController.LoseUI();
                Time.timeScale = 0;
            }
            if (waveIdx >= numWaves)
            {
                if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
                {
                    gameEnd = true;
                    uiController.WinUI();
                    Time.timeScale = 0;
                }
            }
            else if (numKilled >= numEnemies)
            {
                waveIdx++;
                numKilled = 0;
                if (waveIdx < numWaves)
                {
                    numEnemies = GetNumEnemies(waves[waveIdx]);
                    onWaveStart.Invoke(timeBeforeNextWave, waves[waveIdx], home);
                    Invoke("UpdateWave", timeBeforeNextWave);
                }
            }
        }
    }

    int GetNumEnemies(Wave wave)
    {
        int num = 0;
        foreach (EnemyGroup enemyGroup in wave.enemies)
        {
            num += enemyGroup.numEnemy;
        }
        return num * numspawners;
    }

    void UpdateWave()
    {
        uiController.UpdateWave(waveIdx + 1);
    }

    public void UpdateNumKilled()
    {
        //print("wat");
        numKilled++;
    }
}
