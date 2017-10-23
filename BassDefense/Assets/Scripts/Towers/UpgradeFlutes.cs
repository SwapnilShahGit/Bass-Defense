using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFlutes : MonoBehaviour {
	
		
	public GameObject UpgradedFlute;
    public GameObject[] Flutes;
    public BuildTower[] Tiles;
    public Vector2[] Groups;
    int k;
    Vector2 d1;
    Vector2 d2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Flutes = GameObject.FindGameObjectsWithTag("Flute");
        Tiles = GameObject.FindObjectsOfType(typeof(BuildTower)) as BuildTower[];


        Groups = new Vector2[Flutes.Length];
        for (int i = 0; i < Flutes.Length; i++)
        {
            k = 0;
            d1 = Flutes[i].GetComponent<Transform>().position;
            for (int j = 0; j < Flutes.Length; j++)
            {

                d2 = Flutes[j].GetComponent<Transform>().position;

                if (Vector2.Distance(d1, d2) < 2f)
                {
                    k++;
                }

            }
            if (k >= 3)
            {
                Groups[i] = d1;
            }
        }


        foreach (Vector2 pos in Groups){
            foreach (GameObject flute in Flutes)
                {
                    if (Vector2.Distance(pos, flute.GetComponent<Transform>().position) < 2f)
                    {
                        foreach(BuildTower tile in Tiles){
                            if (tile.GetComponent<Transform>().position == flute.GetComponent<Transform>().position)
                            {
                                tile.GetComponent<BuildTower>().placed = 0;
                            }
                        }
                        Destroy(flute);
                    }
                }
                BuildTower t = null;
                foreach (BuildTower tile in Tiles)
                {
                    if (((Vector2)tile.t.position  == pos) )
                    
                    {
                        t = tile;
                    }

                }
                if (t != null)
                {
                    GameObject d = Instantiate(UpgradedFlute, t.t);

                    t.placed = 1;
                    d.transform.localScale = new Vector3(0.50f, 0.50f, 1);
                }
            }
	}
}
