using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform tilePrefab;
    
    [Range(0,100)]
    public int width;
    [Range(0, 100)]
    public int height;

    int[,] map;

    void Start() {
        GenerateMap();
    }

    public void GenerateMap() {
        map = new int[width, height];
    }
}
