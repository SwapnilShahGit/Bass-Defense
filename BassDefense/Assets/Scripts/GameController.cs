using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    // Generate from sprite?
    public bool generateFromSprite;

    // Events
    public UnityEvent onGameStart;
    public UnityEvent onGameEnd;

    GameObject home;

    // UI variables
    public float levelStartDelay = 2f;                          // Num seconds to have overlay display
    private string timeperiod = "60,000 Years Ago";
    private Text timeperiodText;                                //Text to display current age/year
    private GameObject loadingOverlay;                          //Image to block out level as levels are being set up, background for playerHealthText.
    private Text playerHealthText;

    void Start() {
        // Add each sprite into the dictionary for quick referencing
        spriteDictionary = new Dictionary<Color32, Transform[]>();
        foreach(TileSprite tileSprite in tileSprites) {
            spriteDictionary.Add(tileSprite.color, tileSprite.spriteArray);
        }

        Vector2 baseStart = new Vector2(0, 0);
        //Get a reference to our canvas things and show stuff
        loadingOverlay = GameObject.Find("LoadingOverlay");
        timeperiodText = GameObject.Find("YearText").GetComponent<Text>();

        timeperiodText.text = timeperiod;
        loadingOverlay.SetActive(true);
        Invoke("HideLoadingOverlay", levelStartDelay);

        // Generate the map
        if(generateFromSprite && mapSprite != null && tileSprites != null) {
            GenerateMapFromSprite();
        }
        else {
            ProceduralGenerator gen = GenerateMapFromRandom();
            baseStart = new Vector2(-width / 2 + gen.GetBaseLocationX() - 0.5f, -height / 2 + gen.GetBaseLocationY() - 0.5f);
        }

        // Create a base object
        home = Instantiate(basePrefab, baseStart, Quaternion.identity) as GameObject;

        // Create a player object
        player = Instantiate(playerPrefab, baseStart, Quaternion.identity) as GameObject;

        onGameStart.Invoke();
    }

    //Hides black image used between levels
    void HideLoadingOverlay() {
        //Disable the loadingOverlay gameObject.
        loadingOverlay.SetActive(false);
    }
    public void End() {
        onGameEnd.Invoke();
        Destroy(player);
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

    ProceduralGenerator GenerateMapFromRandom() {
        Camera.main.orthographicSize = 9;
        Camera.main.transform.position = new Vector3(-0.5f, -0.5f, -10f);

        ProceduralGenerator mapGen = GetComponent<ProceduralGenerator>();
        width = mapGen.GetWidth();
        height = mapGen.GetHeight();
        Color32[] colorMap = mapGen.GenerateMap();
        GenerateMap(colorMap);
        return mapGen;
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
