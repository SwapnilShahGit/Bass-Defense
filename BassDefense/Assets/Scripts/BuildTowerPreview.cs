using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowerPreview : MonoBehaviour {

    public GameObject[] towerSpritePreviews;
    public GameObject[] towerSprites;
    public LayerMask offPathLayer;

    int towerIdx;
    GameObject towerSpritePreview;
    GameObject CurrentBuildInProgress;


    Vector2 mousePos;

    Vector2 mouseRay;
    RaycastHit2D rayHit;

    public float timeBetweenRays;
    float timeLeftTillNextRay;

    public void InstantiateTemplate(int idx) {
        towerIdx = idx;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));

        towerSpritePreview = Instantiate(towerSpritePreviews[towerIdx], transform.position, Quaternion.identity);

        timeLeftTillNextRay = timeBetweenRays;
    }

    public void DestroyTemplate() {
        if(towerSpritePreview != null) {
            Destroy(towerSpritePreview);
        }
        DiscardPlacedTemplate();
    }

    public void DiscardPlacedTemplate() {
        if(CurrentBuildInProgress != null) {
            Destroy(CurrentBuildInProgress);
        }
    }

    public void BuildTower() {
        GameObject towerSprite = towerSprites[towerIdx];
        Instantiate(towerSprite, CurrentBuildInProgress.transform.position, Quaternion.identity);
        Destroy(CurrentBuildInProgress);
    }

    void Update () {
        if(towerSpritePreview != null) {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            towerSpritePreview.transform.position = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));

            if(timeLeftTillNextRay <= 0) {
                mouseRay = Camera.main.ScreenToWorldPoint(transform.position);
                rayHit = Physics2D.Raycast(mouseRay, Vector2.zero, 30f, offPathLayer);

                if(rayHit) {
                    towerSpritePreview.GetComponent<SpriteRenderer>().sprite = towerSpritePreviews[towerIdx].GetComponent<SpriteRenderer>().sprite;
                    towerSpritePreview.GetComponent<SpriteRenderer>().color = towerSpritePreviews[towerIdx].GetComponent<SpriteRenderer>().color;
                }
                else {
                    towerSpritePreview.GetComponent<SpriteRenderer>().sprite = towerSpritePreviews[towerIdx + 2].GetComponent<SpriteRenderer>().sprite;
                    towerSpritePreview.GetComponent<SpriteRenderer>().color = towerSpritePreviews[towerIdx + 2].GetComponent<SpriteRenderer>().color;
                }
                timeLeftTillNextRay = timeBetweenRays;
            }
            else {
                timeLeftTillNextRay -= Time.deltaTime;
            }

            if(Input.GetMouseButtonDown(0)) {
                mouseRay = Camera.main.ScreenToWorldPoint(transform.position);
                rayHit = Physics2D.Raycast(mouseRay, Vector2.zero, 30f, offPathLayer);

                if(rayHit.collider != null) {
                    if(rayHit.collider.gameObject.tag == "Off Path") {
                        CurrentBuildInProgress = Instantiate(towerSpritePreview, towerSpritePreview.transform.position, Quaternion.identity);
                    }
                }
            }
        }
	}
}
