using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {
    public Slider slider;
    public InputField inputField;

    // Use this for initialization
    void Start () {
        inputField.text = slider.value.ToString();
	}

    public void OnSliderChange() {
        inputField.text = slider.value.ToString();
    }

    public void OnInputChange() {
        float val = slider.value;

        if(float.TryParse(inputField.text, out val)) {
            slider.value = val;
        }
        else {
            inputField.text = slider.value.ToString();
        }
    }
	
	
}
