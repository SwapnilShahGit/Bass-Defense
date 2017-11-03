using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Texture2D cursor;

    string timeperiod = "60,000 Years Ago";
    public Text timeperiodText;
    public Text textRemove;
    public Text waveText;
    public GameObject lossTextRemove;
    public GameObject loadingOverlay;                          
    public AudioSource gongeffect;                         

    public void Initialize(bool isTut, float levelStartDelay) {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        lossTextRemove.SetActive(false);

        if(isTut) {
            timeperiodText.text = "Tutorial";
        }
        else {
            timeperiodText.text = timeperiod;
        }

        loadingOverlay.SetActive(true);
        Invoke("HideLoadingOverlay", levelStartDelay);
    }

    public void LoseUI() {
        print("dead");
        timeperiodText.text = "";
        textRemove.text = "You Lose";
        loadingOverlay.SetActive(true);
        lossTextRemove.SetActive(true);
    }

    public void WinUI() {
        print("win");
        textRemove.text = "Congratulations, you win!";
        timeperiodText.text = "";
        loadingOverlay.SetActive(true);
        lossTextRemove.SetActive(true);
        gongeffect.Play();
    }

    //Hides black image used between levels
    void HideLoadingOverlay() {
        //Disable the loadingOverlay gameObject.
        loadingOverlay.SetActive(false);
    }
}
