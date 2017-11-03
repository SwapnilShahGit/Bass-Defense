using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // is current level tutorial?
    public bool isTut;

    // prefabs
    public GameObject playerPrefab;
    public GameObject basePrefab;

    // Navigation grid
    public Grid grid;

    // Num seconds to have overlay display
    public float levelStartDelay = 2f;                          

    void Start()
    {
        // Get controllers
        WaveController waveController = GetComponent<WaveController>();
        UIController uiController = GetComponent<UIController>();
        MapGeneratorController mapGenController = GetComponent<MapGeneratorController>();

        // Initialize UI
        uiController.Initialize(isTut, levelStartDelay);

        // Initialize the map
        mapGenController.Initialize();

        // Initialize the movement grid
        grid.StartCreatingGrid();

        // Get the start location of the base and references to the spawners
        Vector2 baseStart = mapGenController.GetBaseStart();
        List<EnemySpawner> spawners = mapGenController.GetSpawners();

        // Create a base object
        Transform home = Instantiate(basePrefab, baseStart, Quaternion.identity).transform as Transform;

        // Create a player object
        Instantiate(playerPrefab, baseStart, Quaternion.identity);

        // Start the waves
        waveController.Initialize(home, spawners);
        waveController.StartWaves();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
