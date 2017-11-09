using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Texture2D cursor;

    public string timeperiod;
    public Text OverlayText;
    public Text waveText;
    public GameObject buttons;
    public GameObject overlay;                          
    public AudioSource gongeffect;
    private bool tut;

    public void Initialize(bool isTut, float levelStartDelay) {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        buttons.SetActive(false);
        tut = isTut;
        if(isTut) {
            OverlayText.text = "Defend Your Fire";
        }
        else {
            OverlayText.text = timeperiod;
        }

        overlay.SetActive(true);
        Invoke("HideLoadingOverlay", levelStartDelay);
    }

    public void LoseUI() {
        print("dead");
        OverlayText.text = "You Lose";
        overlay.SetActive(true);
        buttons.SetActive(true);
    }

    public void WinUI() {
        if (tut)
        {
            print("tutwin");
            SceneManager.LoadScene("Prehistoric Era");
        }
        else
        {
            print("win");
            OverlayText.text = "Congratulations, you win!";
            overlay.SetActive(true);
            buttons.SetActive(true);
            gongeffect.Play();
        }
    }

    public void UpdateWave(int waveNum) {
        waveText.text = "Wave: " + waveNum;
        gongeffect.Play();
    }

    //Hides black image used between levels
    void HideLoadingOverlay() {
        //Disable the loadingOverlay gameObject.
        overlay.SetActive(false);
    }
}
