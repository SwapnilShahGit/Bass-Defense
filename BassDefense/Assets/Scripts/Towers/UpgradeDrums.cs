using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDrums : MonoBehaviour
{
    public GameObject UpgradedDrum;
    public GameObject[] Drums;
    public BuildTower[] Tiles;
    public Vector2[] Groups;
    int k;
    Vector2 d1;
    Vector2 d2;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Drums = GameObject.FindGameObjectsWithTag("Drum");
        Tiles = GameObject.FindObjectsOfType(typeof(BuildTower)) as BuildTower[];

        Groups = new Vector2[Drums.Length];
        for (int i = 0; i < Drums.Length; i++)
        {
            Groups[i] = new Vector2(999, 999);
        }

        for (int i = 0; i < Drums.Length; i++)
        {
            k = 0;
            d1 = Drums[i].GetComponent<Transform>().position;
            for (int j = 0; j < Drums.Length; j++)
            {

                d2 = Drums[j].GetComponent<Transform>().position;

                if (Vector2.Distance(d1, d2) < 2f)
                {
                    k++;
                }

            }
            if (k >= 3)
            {
                bool isGrouped = false;
                for (int r = 0; r < i; r++)
                {
                    if (Vector2.Distance(Groups[r], d1) < 2f)
                    {
                        isGrouped = true;
                    }
                }
                if (!isGrouped)
                {
                    Groups[i] = d1;
                }
            }
        }


        foreach (Vector2 pos in Groups)
        {
            foreach (GameObject drum in Drums)
            {
                if (Vector2.Distance(pos, drum.GetComponent<Transform>().position) < 2f)
                {
                    foreach (BuildTower tile in Tiles)
                    {
                        if (tile.GetComponent<Transform>().position == drum.GetComponent<Transform>().position)
                        {
                            tile.GetComponent<BuildTower>().placed = 0;
                        }
                    }
                    Destroy(drum);
                }
            }
            BuildTower t = null;
            foreach (BuildTower tile in Tiles)
            {
                if (((Vector2)tile.t.position == pos))

                {
                    t = tile;
                }

            }
            if (t != null && t.buildable)
            {
                GameObject d = Instantiate(UpgradedDrum, t.t);

                t.placed = 1;
                d.transform.localScale = new Vector3(0.50f, 0.50f, 1);
            }
        }
    }
}
