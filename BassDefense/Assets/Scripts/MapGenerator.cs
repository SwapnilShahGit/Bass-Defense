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

	float tileSpacing;

    void Start() {
		Debug.Log ("Map Start called");
        if(mapSprite != null && tileSprites != null) {
            width = mapSprite.width;
            height = mapSprite.height;

            spriteDictionary = new Dictionary<Color32, GameObject[]>();
            foreach(TileSprite tileSprite in tileSprites) {
                spriteDictionary.Add(tileSprite.color, tileSprite.spriteArray);
            }

			GameObject sprite = tileSprites [0].spriteArray [0];
			Vector2 sprite_size = sprite.GetComponent<SpriteRenderer>().sprite.rect.size;
			Vector2 local_sprite_size = sprite_size / sprite.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

			tileSpacing = local_sprite_size.x;
			Debug.Log ("Done Map Start");
        }
		GenerateMap();
    }

    public void GenerateMap() {
		Debug.Log ("Generating Map (" + width + "," + height + ")");
        Color32[] pixelColors = mapSprite.GetPixels32();

        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                SpawnTile(pixelColors[(y * width) + x], x, y);
            }
        }
		Debug.Log ("Done Generating Map");
    }

    void SpawnTile(Color32 color, int x, int y) {
        if(spriteDictionary.ContainsKey(color)){
            GameObject[] spriteArray = spriteDictionary[color];
            int idx = 0;
            if(spriteArray.Length > 1) {
                idx = UnityEngine.Random.Range(0, spriteArray.Length);
            }
			Debug.Log ("Instantiating Tile (" + x + "," + y + ")");
            GameObject tilePrefab = spriteArray[idx];
			Vector2 tilePosition = new Vector2(-width / 2 + (x * tileSpacing), -height / 2 + (y * tileSpacing));
            GameObject tileSprite = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as GameObject;
        }
        else {
            Debug.LogError("No sprite for color: " + color.ToString());
        }


    }


}
