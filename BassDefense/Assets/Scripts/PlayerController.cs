using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    // UI tingz
    public Text playerHealthText;
    public Text playerMoneyText;

    Vector2 target;
    Vector3[] path;
    int targetIndex;
    int towerIdx;
    bool goingToBuild;
    string mode = "Slashy";


    BuildTowerPreview preview;
    public int speed;
	public GameObject[] towerSprites;
	public int money;
    public int health;

	void Start () {
        preview = (BuildTowerPreview)FindObjectOfType(typeof(BuildTowerPreview));
        towerIdx = 0;

		money = 1;
        health = 99;
        playerHealthText = GameObject.Find("PlayerHealth").GetComponent<Text>();
        playerMoneyText = GameObject.Find("Money").GetComponent<Text>();
        playerHealthText.text = health.ToString();
        playerMoneyText.text = money.ToString();
    }

    void Update () {
        // Change Tower
		if (Input.GetKeyDown ("e")) {
            EnterBuildMode();
            towerIdx = 0;
		}
        if (Input.GetKeyDown("q")) {
            EnterBuildMode();
            towerIdx = 1;
        }

        // Change Mode
        if(Input.GetKeyDown("f")) {
            if(mode == "Build") {
                ExitBuildMode();
            }
            else {
                EnterBuildMode();
            }
        }


        if (mode == "Slashy"){

            // Move
            if (Input.GetMouseButton(1)){
                target = Input.mousePosition;
                GoToTarget(target);
            }

            // Attack
            if (Input.GetMouseButton(0))
            {
                
                //cast(activeAbility);
            }
        }
        else if (mode == "Build") {

            if(goingToBuild) {
                if((Vector2)transform.position == target) {
                    preview.BuildTower();
                    goingToBuild = false;
                }
            }

            if (Input.GetMouseButton(0)){
                if(goingToBuild) {
                    preview.DiscardPlacedTemplate();
                }

                goingToBuild = true;
                target = Input.mousePosition;
                GoToTarget(target);
            }

            if (Input.GetMouseButton(1)){
                goingToBuild = false;
                ExitBuildMode();
                target = Input.mousePosition;
                GoToTarget(target);
            }
        }
        playerHealthText.text = health.ToString();
        playerMoneyText.text = money.ToString();
    }

    void EnterBuildMode() {
        mode = "Build";
        preview.DiscardPlacedTemplate();
        preview.InstantiateTemplate(towerIdx);
    }

    void ExitBuildMode() {
        mode = "Slashy";
        preview.DestroyTemplate();
    }

    public void GoToTarget(Vector3 target) {
        PathRequestManager.RequestPath(transform.position, target, OnPathFound, false);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if(pathSuccessful) {
            path = newPath;
            if(this != null) {
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }
        else {
            Debug.Log("Path unsuccessful");
        }
    }

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];

        while(true) {
            if(transform.position == currentWaypoint) {
                targetIndex++;
                if(targetIndex >= path.Length) {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    void Harmonize()
    {

    }
}
