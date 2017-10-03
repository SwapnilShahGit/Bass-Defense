using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour {
	public GameObject drum;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void OnMouseUp ()
	{
		if (PlayerController.mode == "Build") {
			GameObject d = Instantiate (drum, this.transform);
			d.transform.localScale = new Vector3 (0.50f, 0.50f, 1);

			//d.transform.localScale -= new Vector3(0.95f,0.95f,0);
		}
	}
}
