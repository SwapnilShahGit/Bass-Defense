using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class TileSprite {
    public Color32 color;
    public GameObject[] spriteArray;
}

public class MapGenerator : MonoBehaviour {
    public Texture2D mapSprite;
    public TileSprite[] tileSprites;

    [Range(1,100)]
    public int width;
    [Range(1, 100)]
    public int height;
    Dictionary<Color32, GameObject[]> spriteDictionary;

    void Start() {
        if(mapSprite != null && tileSprites != null) {
            width = mapSprite.width;
            height = mapSprite.height;

            spriteDictionary = new Dictionary<Color32, GameObject[]>();
            foreach(TileSprite tileSprite in tileSprites) {
                spriteDictionary.Add(tileSprite.color, tileSprite.spriteArray);
            }
        }
        GenerateMap();
    }

    public void GenerateMap() {
        Color32[] pixelColors = mapSprite.GetPixels32();

        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                SpawnTile(pixelColors[(y * width) + x], x, y);
            }
        }
        Debug.Log("done");
    }

    void SpawnTile(Color32 color, int x, int y) {
        if(spriteDictionary.ContainsKey(color)){
            GameObject[] spriteArray = spriteDictionary[color];
            int idx = 0;
            if(spriteArray.Length > 1) {
                idx = UnityEngine.Random.Range(0, spriteArray.Length);
            }
            GameObject tilePrefab = spriteArray[idx];
            Vector2 tilePosition = new Vector2(-width / 2 + 0.5f + x, -height / 2 + 0.5f + y);
            GameObject tileSprite = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as GameObject;
        }
        else {
            Debug.LogError("No sprite for color: " + color.ToString());
        }


    }
}
