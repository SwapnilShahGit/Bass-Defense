using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StartEvent : UnityEvent<float, int, Transform> { }

public class WaveController : MonoBehaviour {
    //Events
    public StartEvent onWaveStart;
    public UnityEvent onWaveEnd;

    // Time
    float currentTime;
    public float secondsBetweenWaves;

    bool[] invoked = { false, false, false, false, false };

    UIController uIController;
    Transform home;

    public void StartWaves(Transform _home, List<EnemySpawner> spawners) {
        uIController = GetComponent<UIController>();
        home = _home;

        foreach(EnemySpawner spawner in spawners) {
            onWaveStart.AddListener(spawner.StartSpawning);
            onWaveEnd.AddListener(spawner.StopSpawning);
        }

    }
    
    /*
    void Update() {
        if(BaseController.hp <= 0 || PlayerController.health <= 0) {
            uIController.LoseUI();
        }
        if(currentTime >= secondsBetweenWaves * 2 && isTut) {
            SceneManager.LoadScene("Prehistoric Era");
        }

        if(currentTime >= secondsBetweenWaves * 5) {
            uIController.WinUI();
            Debug.Log(currentTime + " end");
        }
        else if(currentTime >= secondsBetweenWaves * 4 && !invoked[4]) {
            onWaveEnd.Invoke();
            onWaveStart.Invoke(levelStartDelay + 1f, 4, home);
            invoked[4] = true;
            waveText.text = "Wave: 5";
            gongeffect.Play();
            Debug.Log(currentTime + " Wave 5");
        }
        else if(currentTime >= secondsBetweenWaves * 3 && !invoked[3]) {
            onWaveEnd.Invoke();
            onWaveStart.Invoke(levelStartDelay + 1f, 3, home);
            invoked[3] = true;
            waveText.text = "Wave: 4";
            gongeffect.Play();
            Debug.Log(currentTime + " Wave 4");
        }
        else if(currentTime >= secondsBetweenWaves * 2 && !invoked[2]) {
            onWaveEnd.Invoke();
            onWaveStart.Invoke(levelStartDelay + 1f, 2, home);
            invoked[2] = true;
            waveText.text = "Wave: 3";
            gongeffect.Play();
            Debug.Log(currentTime + " Wave 3");
        }
        else if(currentTime >= secondsBetweenWaves && !invoked[1]) {
            onWaveEnd.Invoke();
            onWaveStart.Invoke(levelStartDelay + 1f, 1, home);
            invoked[1] = true;
            waveText.text = "Wave: 2";
            gongeffect.Play();
            Debug.Log(currentTime + " Wave 2");
        }

        currentTime += Time.deltaTime;
    }
    */
}
