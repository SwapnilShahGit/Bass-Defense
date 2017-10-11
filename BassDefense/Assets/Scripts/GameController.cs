using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TileSprite {
    public Color32 color;
    public Transform[] spriteArray;
}

public class GameController : MonoBehaviour {

    // player variables
	public GameObject playerPrefab;

    GameObject player;

    // Map variables
    public Texture2D mapSprite;
    public TileSprite[] tileSprites;

    Dictionary<Color32, Transform[]> spriteDictionary;
    int width, height;
    float tileSpacing;
    Transform mapHolder;

    // Base variables
    public GameObject basePrefab;

    //Events
    public UnityEvent onGameStart;
    public UnityEvent onGameEnd;

    GameObject home;

    void Start() {
        // Generate the map
        if(mapSprite != null && tileSprites != null) {
            GenerateMapFromSprite();
        }
        else {
            GenerateMapFromRandom();
        }

        // Create a base object
        home = Instantiate(basePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        // Create a player object
        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        onGameStart.Invoke();
    }

	void Update() {
		
	}

    public void End() {
        onGameEnd.Invoke();
        Destroy(player);
    }

    void GenerateMapFromSprite() {
        // Get the dimensions of the map from sprite size
        width = mapSprite.width;
        height = mapSprite.height;

        // Add each sprite into the dictionary for quick referencing
        spriteDictionary = new Dictionary<Color32, Transform[]>();
        foreach(TileSprite tileSprite in tileSprites) {
            spriteDictionary.Add(tileSprite.color, tileSprite.spriteArray);
        }

        GenerateMap();
    }

    void GenerateMapFromRandom() {
        
        GenerateMap();
    }


    void GenerateMap() {
        mapHolder = new GameObject("Generated Map").transform;
        mapHolder.parent = transform;

        Debug.Log("Generating Map (" + width + "," + height + ")");
        // Get an array of pixel colors from the sprite map
        Color32[] pixelColors = mapSprite.GetPixels32();

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
            Transform[] spriteArray = spriteDictionary[color];
            int idx = 0;
            if(spriteArray.Length > 1) {
                idx = UnityEngine.Random.Range(0, spriteArray.Length);
            }
            Transform tilePrefab = spriteArray[idx];
            Vector2 tilePosition = new Vector2(-width / 2 + x, -height / 2 + y);
            Transform tileSprite = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as Transform;
            tileSprite.parent = mapHolder;
        }
        else {
            Debug.LogError("No sprite for color: " + color.ToString());
        }


    }

}
