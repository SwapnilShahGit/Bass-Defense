using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameLevel
{
    public Image arrow;
    public GameObject levelSummary;
    public string sceneName;
}

public class MenuNavigation : MonoBehaviour
{

    // Menu variables
    public GameObject mainMenu;
    public GameObject storyModeMenu;
    public GameObject randomModeMenu;
    public GameObject settingsMenu;
    bool quitPanelActive = false;

    GameObject currentMenu;

    // Story Mode variables
    public GameLevel[] gameLevels;
    public Button playButton;
    public GameLevel currentLevelSelected;

    // Random Mode variables
    public MapPreview mapPreview;
    public ProceduralGenerator gen;
    public Slider[] sliders;
    public GameObject cover;
    System.Random rng;

    // Quit pannel variables
    public GameObject panel;

    void Start()
    {
        currentMenu = mainMenu;
        currentMenu.SetActive(true);

        rng = new System.Random();
    }

    // Main Menu
    public void GoToMainMenu()
    {
        if (!quitPanelActive)
        {
            currentLevelSelected.levelSummary.SetActive(false);
            currentLevelSelected.arrow.gameObject.SetActive(false);

            currentMenu.SetActive(false);
            mainMenu.SetActive(true);
            currentMenu = mainMenu;
        }
    }

    // Story Mode
    public void GoToStoryMenu()
    {
        if (!quitPanelActive)
        {
            currentMenu.SetActive(false);
            storyModeMenu.SetActive(true);
            currentMenu = storyModeMenu;

            currentLevelSelected = gameLevels[0];
            SelectPrehistoric();
        }
    }

    public void SelectPrehistoric()
    {
        PlayerController.era = "pre";
        currentLevelSelected.levelSummary.SetActive(false);
        currentLevelSelected.arrow.gameObject.SetActive(false);

        currentLevelSelected = gameLevels[0];
        currentLevelSelected.levelSummary.SetActive(true);
        currentLevelSelected.arrow.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
    }

    public void SelectClassical()
    {
        PlayerController.era = "classical";
        currentLevelSelected.levelSummary.SetActive(false);
        currentLevelSelected.arrow.gameObject.SetActive(false);

        currentLevelSelected = gameLevels[1];
        currentLevelSelected.levelSummary.SetActive(true);
        currentLevelSelected.arrow.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
    }

    public void SelectModern()
    {
        currentLevelSelected.levelSummary.SetActive(false);
        currentLevelSelected.arrow.gameObject.SetActive(false);

        currentLevelSelected = gameLevels[2];
        currentLevelSelected.levelSummary.SetActive(true);
        currentLevelSelected.arrow.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
    }

    public void SelectPlay()
    {
        //SceneManager.LoadScene(currentLevelSelected.sceneName);
        if (currentLevelSelected == gameLevels[0])
        {
            SceneManager.LoadScene("Controls");
        }
        else
        {
            SceneManager.LoadScene(currentLevelSelected.sceneName);
        }

    }

    // Random Generation Mode
    public void GoToRandomMenu()
    {
        if (!quitPanelActive)
        {
            currentMenu.SetActive(false);
            randomModeMenu.SetActive(true);
            currentMenu = randomModeMenu;

            GeneratePredefinedPreview();
        }
    }


    public void GeneratePredefinedPreview()
    {
        gen.numSpawners = (int)sliders[0].value;
        gen.minPathLength = (int)sliders[1].value;
        gen.MaxPathLength = (int)sliders[2].value;
        gen.percentObstacles = sliders[3].value;
        gen.mapSeed = (int)sliders[4].value;

        if (gen.minPathLength > gen.MaxPathLength)
        {
            cover.SetActive(true);
        }
        else
        {
            cover.SetActive(false);
            mapPreview.GeneratePreview();
        }
    }

    public void GenerateRandomPreview()
    {
        sliders[0].value = rng.Next(1, 5);
        sliders[1].value = rng.Next(3, 40);
        sliders[2].value = rng.Next(gen.minPathLength, 40);
        sliders[3].value = (float)(rng.NextDouble() * (0.8));
        sliders[4].value = rng.Next(1, 1001);

        GeneratePredefinedPreview();
    }

    public void PlayRandom()
    {
        SceneManager.LoadScene("Random Map");
    }



    // Settings
    public void GoToSettingsMenu()
    {
        if (!quitPanelActive)
        {
            currentMenu.SetActive(false);
            settingsMenu.SetActive(true);
            currentMenu = settingsMenu;
        }
    }

    // Quit
    public void GetQuitPanel()
    {
        panel.SetActive(true);
        quitPanelActive = true;
    }

    public void PressYes()
    {
        Application.Quit();
    }

    public void PressNo()
    {
        panel.SetActive(false);
        quitPanelActive = false;
    }
}
