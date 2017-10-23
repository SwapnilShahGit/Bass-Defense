using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject pauseMenu;

    bool isActive;
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyUp(KeyCode.Escape)) {
            if(isActive) {
                isActive = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else {
                isActive = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
	}

    public void Continue() {
        isActive = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToMenu() {
        SceneManager.LoadScene("Menus");
    }

    public void GoToDesktop() {
        Application.Quit();
    }
}
