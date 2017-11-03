using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileSprite {
    public Color32 color;
    public GameObject[] spriteArray;
}

public class MapGeneratorController : MonoBehaviour {

    // Generate from sprite?
    public bool generateFromSprite;

    // Map variables
    public Texture2D mapSprite;
    public TileSprite[] tileSprites;

    Dictionary<Color32, GameObject[]> spriteDictionary;
    int width, height;
    float tileSpacing;
    Vector2 baseStart;
    Transform mapHolder;
    List<EnemySpawner> spawners;

    public void Initialize() {
        // Add each sprite into the dictionary for quick referencing
        spriteDictionary = new Dictionary<Color32, GameObject[]>();
        foreach(TileSprite tileSprite in tileSprites) {
            spriteDictionary.Add(tileSprite.color, tileSprite.spriteArray);
        }

        baseStart = new Vector2(0, 0);


        // Generate the map
        if(generateFromSprite && mapSprite != null && tileSprites != null) {
            GenerateMapFromSprite();
        }
        else {
            GenerateMapFromRandom();
            baseStart = new Vector2(-width / 2 + MapData.BaseLocationX - 0.5f, -height / 2 + MapData.BaseLocationY - 0.5f);
        }


    }

    void GenerateMapFromSprite() {
        Camera.main.orthographicSize = 8;
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);

        // Get the dimensions of the map from sprite size
        width = mapSprite.width;
        height = mapSprite.height;

        Color32[] pixelColors = mapSprite.GetPixels32();
        GenerateMap(pixelColors);
    }

    void GenerateMapFromRandom() {
        Camera.main.orthographicSize = 9;
        Camera.main.transform.position = new Vector3(-0.5f, -0.5f, -10f);

        width = MapData.Width;
        height = MapData.Height;
        Color32[] colorMap = MapData.ColorMap;
        GenerateMap(colorMap);
    }


    void GenerateMap(Color32[] pixelColors) {
        mapHolder = new GameObject("Generated Map").transform;
        mapHolder.parent = transform;

        // Match each color in the array with a corresponding tile and create it in the game world
        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                SpawnTile(pixelColors[(y * width) + x], x, y);
            }
        }
        Debug.Log("Done Generating Map");
    }

    void SpawnTile(Color32 color, int x, int y) {
        if(spriteDictionary.ContainsKey(color)) {
            GameObject[] spriteArray = spriteDictionary[color];
            int idx = 0;
            if(spriteArray.Length > 1) {
                idx = UnityEngine.Random.Range(0, spriteArray.Length);
            }
            GameObject tilePrefab = spriteArray[idx];
            Vector2 tilePosition = new Vector2(-width / 2 + x, -height / 2 + y);
            GameObject tileSprite = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as GameObject;
            tileSprite.transform.parent = mapHolder;

            EnemySpawner spawner = tileSprite.GetComponent<EnemySpawner>();
            if(spawner != null) {
                spawners.Add(spawner);
            }
        }
        else {
            Debug.LogError("No sprite for color: " + color.ToString());
        }
    }

    public Vector2 GetBaseStart() {
        return baseStart;
    }

    public List<EnemySpawner> GetSpawners() {
        return spawners;
    }
}
