using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsNavigation : MonoBehaviour {

	public void GoBack() {
        SceneManager.LoadScene("Menus");
    }

    public void GoPlay() {
        SceneManager.LoadScene("Prehistoric Era");
    }

}
