using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameLevel {
    public Image arrow;
    public GameObject levelSummary;
    public string sceneName;
}

[System.Serializable]
public class VariableSlider {
    public Slider slider;
    public InputField inputField;
}

public class MenuNavigation : MonoBehaviour {

    // Menu public variables
    public GameObject mainMenu;
    public GameObject storyModeMenu;
    public GameObject randomModeMenu;
    public GameObject settingsMenu;

    GameObject currentMenu;

    // Story Mode public variables
    public GameLevel[] gameLevels;
    public Button playButton;
    public GameLevel currentLevelSelected;

    // Random Mode public variables
    public MapPreview mapPreview;
    public VariableSlider[] sliders;
    
    void Start() {
        currentMenu = mainMenu;
        currentMenu.SetActive(true);
    }

    // Main Menu
    public void GoToMainMenu() {
        currentMenu.SetActive(false);
        mainMenu.SetActive(true);
        currentMenu = mainMenu;
    }

    // Story Mode
    public void GoToStoryMenu() {
        currentMenu.SetActive(false);
        storyModeMenu.SetActive(true);
        currentMenu = storyModeMenu;

        currentLevelSelected = gameLevels[0];
        SelectPrehistoric();
    }

    public void SelectPrehistoric() {
        currentLevelSelected.levelSummary.SetActive(false);
        currentLevelSelected.arrow.gameObject.SetActive(false);

        currentLevelSelected = gameLevels[0];
        currentLevelSelected.levelSummary.SetActive(true);
        currentLevelSelected.arrow.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
    }

    public void SelectClassical() {
        currentLevelSelected.levelSummary.SetActive(false);
        currentLevelSelected.arrow.gameObject.SetActive(false);

        currentLevelSelected = gameLevels[1];
        currentLevelSelected.levelSummary.SetActive(true);
        currentLevelSelected.arrow.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
    }

    public void SelectModern() {
        currentLevelSelected.levelSummary.SetActive(false);
        currentLevelSelected.arrow.gameObject.SetActive(false);

        currentLevelSelected = gameLevels[2];
        currentLevelSelected.levelSummary.SetActive(true);
        currentLevelSelected.arrow.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
    }

    public void SelectPlay() {
        SceneManager.LoadScene(currentLevelSelected.sceneName);
    }

    // Random Generation Mode
    public void GoToRandomMenu() {
        currentMenu.SetActive(false);
        randomModeMenu.SetActive(true);
        currentMenu = randomModeMenu;

        mapPreview.GeneratePreview();
    }

    // Settings
    public void GoToSettingsMenu() {
        currentMenu.SetActive(false);
        settingsMenu.SetActive(true);
        currentMenu = settingsMenu;
    }

    // Quit
}
