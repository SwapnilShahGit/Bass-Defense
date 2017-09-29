using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject playerPrefab;

	GameObject player;

	void Start() {
		player = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
	}

	void Update() {
		
	}
}
