using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramophoneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerController.flow -= 40;
        PlayerController.flowregen += 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
