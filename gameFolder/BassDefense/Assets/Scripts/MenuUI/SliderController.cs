using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {
    public Slider slider;
    public InputField inputField;
    public Text textField;

    // Use this for initialization
    void Start () {
        if(inputField != null) {
            inputField.text = slider.value.ToString();
        }
        if(textField != null) {
            textField.text = ConvertNumToEra(slider.value);
        }
    }

    public string ConvertNumToEra(float num) {
        if(num == 1) {
            PlayerController.era = "pre";
            return "Prehistoric";
        }
        else if(num == 2) {
            PlayerController.era = "classical";
            return "Classical";
        }
        else {
            PlayerController.era = "modern";
            return "Modern";
        }
    }

    public void OnSliderChange() {
        if(inputField != null) {
            inputField.text = slider.value.ToString();
        }

        if(textField != null) {
            textField.text = ConvertNumToEra(slider.value);
        }
    }

    public void OnInputChange() {
        if(inputField != null) {
            float val = slider.value;

            if(float.TryParse(inputField.text, out val)) {
                slider.value = val;
            }
            else {
                inputField.text = slider.value.ToString();
            }
        }
    }
	
	
}
