using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPreview : MonoBehaviour {

    int width;
    int height;
    GameObject map;
    Transform mapHolder;
    HashSet<Color32> colorSet;

    public Color32[] tileColors;
    public SpriteRenderer previewTilePrefab;

    void Start() {
        colorSet = new HashSet<Color32>();
        foreach(Color32 color in tileColors) {
            colorSet.Add(color);
        }
    }

    public void GeneratePreview() {
        ProceduralGenerator gen = GenerateMapFromRandom();
        Vector2 baseStart = new Vector2(-width / 2 + gen.GetBaseLocationX() - 0.5f, -height / 2 + gen.GetBaseLocationY() - 0.5f);
    }

    ProceduralGenerator GenerateMapFromRandom() {
        ProceduralGenerator mapGen = GetComponent<ProceduralGenerator>();
        width = mapGen.GetWidth();
        height = mapGen.GetHeight();
        Color32[] colorMap = mapGen.GenerateMap();
        GenerateMapPreview(colorMap);

        MapData.Width = width;
        MapData.Height = height;
        MapData.ColorMap = colorMap;
        MapData.BaseLocationX = mapGen.GetBaseLocationX();
        MapData.BaseLocationY = mapGen.GetBaseLocationY();

        return mapGen;
    }

    void GenerateMapPreview(Color32[] pixelColors) {
        if(mapHolder != null) {
            Destroy(map);
        }
        map = new GameObject("Generated Map");
        mapHolder = map.transform;
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
        if(colorSet.Contains(color)) {
            Vector2 tilePosition = new Vector2(-width / 2 + x, -height / 2 + y);
            SpriteRenderer tileSprite = Instantiate(previewTilePrefab, tilePosition, Quaternion.identity) as SpriteRenderer;
            tileSprite.transform.parent = mapHolder;
            tileSprite.color = color;
        }
        else {
            Debug.LogError("No sprite for color: " + color.ToString());
        }


    }

}
