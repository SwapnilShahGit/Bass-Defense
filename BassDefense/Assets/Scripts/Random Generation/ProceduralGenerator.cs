using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour {
    [Range(1, 4)]
    public int numSpawners;

    [Range(3, 32)]
    public int MaxPathLength;
    [Range(3, 32)]
    public int minPathLength;

    [Range(0, 1)]
    public float percentObstacles;

    //[Range(1, 4)]
    int numBases = 1;

    [Range(1, 1000)]
    public int mapSeed;

    public Color32 grassTileColor;
    public Color32 pathTileColor;
    public Color32 obstacleTileColor;
    public Color32 spawnerTileColor;
    public Color32 buildableTileColor;

    public Texture2D[] twoPathMaps;
    public Texture2D[] threePathMaps;
    public Texture2D[] fourPathMaps;

    Color32[] colorPaths;

    Color32[] map;
    Coord baseLocation;

    int mapWidth = 30;
    int mapHeight = 18;

    int sectorWidth;
    int sectorHeight;

    RandomGen gen;

    public struct Coord {
        public int x;
        public int y;

        public Coord(int _x, int _y) {
            x = _x;
            y = _y;
        }
    }

    public int GetWidth() {
        return mapWidth;
    }

    public int GetHeight() {
        return mapHeight;
    }
    
    Coord SectorToCoord(int sector) {
        int x = sectorWidth * (sector % 3);
        int y = sectorHeight * (sector / 3);

        return new Coord(x, y);
    }

    int CoordToSector(Coord c) {
        return (((c.y) / sectorHeight) * 3) + (c.x / sectorWidth);
    }

    public float GetBaseLocationX() {
        return (float)baseLocation.x + (float)sectorWidth / 2f;
    }

    public float GetBaseLocationY() {
        return (float)baseLocation.y + (float)sectorHeight / 2f;
    }

    public Color32[] GenerateMap() {
        gen = new RandomGen(mapSeed);

        FillMap();
        ChooseSectorForBases();
        CreatePaths();
        AddObstacles();

        return map;
    }

    void FillMap() { 
        map = new Color32[mapWidth * mapHeight];

        Color32 currentColor = grassTileColor;
        for(int w = 0; w < mapWidth; w++) {
            for(int h = 0; h < mapHeight; h++) {
                int currentIdx = (h * mapWidth) + w;
                map[currentIdx] = new Color32(currentColor.r, currentColor.g, currentColor.b, currentColor.a);
            }
        }
    }

    void CreateBase(int sector) {
        Coord c = SectorToCoord(sector);
        //Debug.Log(sector);
        //Debug.Log(c.x + " " + c.y );

        for(int w = c.x; w < c.x + sectorWidth; w++) {
            for(int h = c.y; h < c.y + sectorHeight; h++) {

                int currentIdx = (h * mapWidth) + w;
                //Debug.Log(currentIdx);
                map[currentIdx].r = buildableTileColor.r;
                map[currentIdx].g = buildableTileColor.g;
                map[currentIdx].b = buildableTileColor.b;
                map[currentIdx].a = buildableTileColor.a;
            }
        }
        baseLocation = c ;
    }

    void ChooseSectorForBases() {
        sectorWidth = mapWidth / 3;
        sectorHeight = mapHeight / 3;

        Queue<int> sectors = gen.RandomShuffleArray<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8});

        for(int baseNum = 0; baseNum < numBases; baseNum++) {
            CreateBase(sectors.Dequeue());
        }
    }

    bool ColorsEqual(Color32 c1, Color32 c2) {
        return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b && c1.a == c2.a;
    }

    bool CheckAdjacent(int x, int y) {
        int currentIdx;
        int numNotGreen = 0;

        //left
        if(x != 0) {
            currentIdx = (y * mapWidth) + (x - 1);
            if(!ColorsEqual(map[currentIdx], grassTileColor)) {
                numNotGreen += 1;
                if(numNotGreen > 1) {
                    return false;
                }
            }
        }

        //right
        if(x != mapWidth - 1) {
            currentIdx = (y * mapWidth) + (x + 1);
            if(!ColorsEqual(map[currentIdx], grassTileColor)) {
                numNotGreen += 1;
                if(numNotGreen > 1) {
                    return false;
                }
            }
        }

        //down
        if(y != 0) {
            currentIdx = ((y - 1) * mapWidth) + (x);
            if(!ColorsEqual(map[currentIdx], grassTileColor)) {
                numNotGreen += 1;
                if(numNotGreen > 1) {
                    return false;
                }
            }
        }

        //up
        if(y != mapHeight - 1) {
            currentIdx = ((y + 1) * mapWidth) + (x);
            if(!ColorsEqual(map[currentIdx], grassTileColor)) {
                numNotGreen += 1;
                if(numNotGreen > 1) {
                    return false;
                }
            }
        }
        return true;
    }

    List<Coord>[] GetAdjacentGreens(Coord sectorCoord) {
        List<Coord>[] adjacentGreens = new List<Coord>[4];

        List<Coord> top = new List<Coord>();
        int x;
        int y = sectorCoord.y + sectorHeight;
        if(y < mapHeight) {
            for(x = sectorCoord.x; x < sectorCoord.x + sectorWidth; x++) {
                int currentIdx = (y * mapWidth) + x;
                if(ColorsEqual(map[currentIdx], grassTileColor)) {
                    if(CheckAdjacent(x, y)) {
                        top.Add(new Coord(x, y));
                    }
                }
            }
        }
        adjacentGreens[0] = top;

        List<Coord> bottom = new List<Coord>();
        y = sectorCoord.y - 1;
        if(y >= 0) {
            for(x = sectorCoord.x; x < sectorCoord.x + sectorWidth; x++) {
                int currentIdx = (y * mapWidth) + x;
                if(ColorsEqual(map[currentIdx], grassTileColor)) {
                    if(CheckAdjacent(x, y)) {
                        bottom.Add(new Coord(x, y));
                    }
                }
            }
        }
        adjacentGreens[1] = bottom;

        List<Coord> left = new List<Coord>();
        x = sectorCoord.x - 1;
        if(x >= 0) {
            for(y = sectorCoord.y; y < sectorCoord.y + sectorHeight; y++) {
                int currentIdx = (y * mapWidth) + x;
                if(ColorsEqual(map[currentIdx], grassTileColor)) {
                    if(CheckAdjacent(x, y)) {
                        left.Add(new Coord(x, y));
                    }
                }
            }
        }
        adjacentGreens[2] = left;

        List<Coord> right = new List<Coord>();
        x = sectorCoord.x + sectorWidth;
        if(x < mapWidth) {
            for(y = sectorCoord.y; y < sectorCoord.y + sectorHeight; y++) {
                int currentIdx = (y * mapWidth) + x;
                if(ColorsEqual(map[currentIdx], grassTileColor)) {
                    if(CheckAdjacent(x, y)) {
                        right.Add(new Coord(x, y));
                    }
                }
            }
        }
        adjacentGreens[3] = right;

        return adjacentGreens;
    }
    
    void CreatePaths() {
        //Debug.Log("Sector Coords: x: "+ baseLocation.x + ", y: "+ baseLocation.y);
        // find adjacent tiles to the base
        List<Coord>[] adjacentGreens = GetAdjacentGreens(baseLocation);
        int numEdges = 0;
        List<int> edgeIdx = new List<int>();

        // find which edges are empty
        for(int i = 0; i < 4; i++) {
            if(adjacentGreens[i].Count != 0) {
                numEdges++;
                edgeIdx.Add(i);
            }
        }


        // get path maps according to number of paths
        Texture2D[] pathMaps;
        if(numSpawners <= 2) {
            pathMaps = twoPathMaps;
        }
        else if(numSpawners == 3) {
            pathMaps = threePathMaps;
        }
        else {
            pathMaps = fourPathMaps;
        }

        //get the path map for our sector
        Texture2D pathMap = pathMaps[CoordToSector(baseLocation)];
        colorPaths = pathMap.GetPixels32();

        // Shuffle the order in which we build paths from the edges
        Queue<int> edgeOrder =  gen.RandomShuffleArray<int>(edgeIdx.ToArray());
        List<Color32> visitedColors = new List<Color32>();
        //Debug.Log(numEdges);

        for(int i = 0; i < numSpawners; i++) {
            List<Coord> edge = adjacentGreens[edgeOrder.Dequeue()];
            int secondPathEdgeIndex = GetPathsOnEdge(edge);

            //Debug.Log("secondPathEdgeIndex " + secondPathEdgeIndex);
            //Debug.Log("edge count " + edge.Count);

            if(secondPathEdgeIndex > 0) {
                Coord firstCoord = edge[0];
                Coord secondCoord = edge[secondPathEdgeIndex];

                int currentIdx = (firstCoord.y * mapWidth) + firstCoord.x;
                Color32 firstColor = colorPaths[currentIdx];
                currentIdx = (secondCoord.y * mapWidth) + secondCoord.x;
                Color32 secondColor = colorPaths[currentIdx];

                if(!visitedColors.Contains(firstColor)) {
                    if(!VisitColor(edge, 0, secondPathEdgeIndex, firstColor)) {
                        Debug.LogError("Could not form path");
                    }
                    visitedColors.Add(firstColor);
                }

                i++;

                if(!visitedColors.Contains(secondColor)) {
                    if(!VisitColor(edge, secondPathEdgeIndex, edge.Count, secondColor)) {
                        Debug.LogError("Could not form path");
                    }
                    visitedColors.Add(secondColor);
                }
            }
            else {
                Coord firstCoord = edge[0];
                int currentIdx = (firstCoord.y * mapWidth) + firstCoord.x;
                Color32 firstColor = colorPaths[currentIdx];
                Debug.Log(firstColor);
                Debug.Log("edge first coord: " + edge[0].x + ", " + edge[0].y);
                if(!visitedColors.Contains(firstColor)) {
                    if(!VisitColor(edge, 0, edge.Count, firstColor)) {
                        Debug.LogError("Could not form path");
                    }
                    visitedColors.Add(firstColor);
                }
            }
        }
    }

    int GetPathsOnEdge(List<Coord> edge) {
        Coord firstTile = edge[0];
        int currentIdx = (firstTile.y * mapWidth) + firstTile.x;
        Color32 pastColor = colorPaths[currentIdx];
        int num = 0;

        foreach(Coord tile in edge) {
            currentIdx = (tile.y * mapWidth) + tile.x;
            if(!ColorsEqual(colorPaths[currentIdx], pastColor)) {
                return num;
            }
            num++;
        }

        return 0;
    }

    bool VisitColor(List<Coord> edge, int start, int end, Color32 color) {
        Debug.Log("Start: " + start + ", End: " + end);
        List<Coord> secondColorChoices = edge.GetRange(start, end - start);
        Queue<Coord> startTileQ = gen.RandomShuffleArray<Coord>(secondColorChoices.ToArray());
        Coord startTile = startTileQ.Dequeue();

        while(startTileQ.Count != 0 && !CheckAdjacent(startTile.x, startTile.y)) {
            startTile = startTileQ.Dequeue();
        }

        int pathLength = gen.GetRandomNumber(minPathLength, MaxPathLength + 1);

        bool[] visited = new bool[mapHeight * mapWidth];

        return CreatePath(startTile, pathLength, visited, color);
    }

    bool CheckAdjacentVisited(int x, int y, bool[] visited) {
        int idx;
        int numVisited = 0;

        // up
        if(y != mapHeight - 1) {
            idx = ((y + 1) * mapWidth) + (x);
            if(visited[idx]) {
                if(numVisited >= 1) {
                    return false;
                }
                numVisited++;
            }
        }

        // down
        if(y != 0) {
            idx = ((y - 1) * mapWidth) + (x);
            if(visited[idx]) {
                if(numVisited >= 1) {
                    return false;
                }
                numVisited++;
            }
        }

        // left
        if(x != 0) {
            idx = ((y) * mapWidth) + (x - 1);
            if(visited[idx]) {
                if(numVisited >= 1) {
                    return false;
                }
                numVisited++;
            }
        }

        // right
        if(x != mapWidth - 1) {
            idx = ((y) * mapWidth) + (x + 1);
            if(visited[idx]) {
                if(numVisited >= 1) {
                    return false;
                }
                numVisited++;
            }
        }

        return true;
    }

    bool CheckAdjacentPath(int x, int y, bool first) {
        int idx;

        // up
        if(y != mapHeight - 1) {
            idx = ((y + 1) * mapWidth) + (x);
            if(ColorsEqual(map[idx], pathTileColor)) {
                return false;
            }
            if(!first && ColorsEqual(map[idx], buildableTileColor)) {
                return false;
            }
        }

        // down
        if(y != 0) {
            idx = ((y - 1) * mapWidth) + (x);
            if(ColorsEqual(map[idx], pathTileColor)) {
                return false;
            }
            if(!first && ColorsEqual(map[idx], buildableTileColor)) {
                return false;
            }
        }

        // left
        if(x != 0) {
            idx = ((y) * mapWidth) + (x - 1);
            if(ColorsEqual(map[idx], pathTileColor)) {
                return false;
            }
            if(!first && ColorsEqual(map[idx], buildableTileColor)) {
                return false;
            }
        }

        // right
        if(x != mapWidth - 1) {
            idx = ((y) * mapWidth) + (x + 1);
            if(ColorsEqual(map[idx], pathTileColor)) {
                return false;
            }
            if(!first && ColorsEqual(map[idx], buildableTileColor)) {
                return false;
            }
        }

        return true;
    }

    List<Coord> GetAdjacentTiles(int x, int y, bool[] visited) {
        List<Coord> lst = new List<Coord>();
        int idx;

        // up
        if(y < mapHeight - 1) {
            idx = ((y + 1) * mapWidth) + (x);
            if(!visited[idx]) {
                Coord newCoord = new Coord(x, y + 1);
                lst.Add(newCoord);
            }
        }

        // down
        if(y != 0) {
            idx = ((y - 1) * mapWidth) + (x);
            if(!visited[idx]) {
                Coord newCoord = new Coord(x, y - 1);
                lst.Add(newCoord);
            }
        }

        // left
        if(x != 0) {
            idx = ((y) * mapWidth) + (x - 1);
            if(!visited[idx]) {
                Coord newCoord = new Coord(x - 1, y);
                lst.Add(newCoord);
            }
        }

        // right
        if(x < mapWidth - 1) {
            idx = ((y) * mapWidth) + (x + 1);
            if(!visited[idx]) {
                Coord newCoord = new Coord(x + 1, y);
                lst.Add(newCoord);
            }
        }
        return lst;
    }


    bool CreatePath(Coord tile, int pathLength, bool[] visited, Color32 pathColor, bool first = true) {
        int currentIdx = (tile.y * mapWidth) + tile.x;
        
        if(!ColorsEqual(colorPaths[currentIdx], pathColor)) {
            //Debug.Log("colorPaths return");
            return false;
        }

        if(!CheckAdjacentPath(tile.x, tile.y, first)) {
            //Debug.Log("adj paths return");
            return false;
        }

        if(!CheckAdjacentVisited(tile.x, tile.y, visited)) {
            //Debug.Log("visited return");
            return false;
        }

        if(pathLength == 1) {
            map[currentIdx].r = spawnerTileColor.r;
            map[currentIdx].g = spawnerTileColor.g;
            map[currentIdx].b = spawnerTileColor.b;
            map[currentIdx].a = spawnerTileColor.a;

            //Debug.Log("End of color: (" + pathColor.r + ", " + pathColor.g + ", " + pathColor.b + "): x: " + tile.x + " y:" + tile.y);
            return true;
        }

        visited[currentIdx] = true;

        List<Coord> adjacentTiles = GetAdjacentTiles(tile.x, tile.y, visited);
        Queue<Coord> adjacentQueue = gen.RandomShuffleArray<Coord>(adjacentTiles.ToArray());

        while(adjacentQueue.Count != 0) {
            Coord nextTile = adjacentQueue.Dequeue();
            //Debug.Log("Next Tile  x: " + nextTile.x + ", y: " + nextTile.y);
            if(nextTile.x < 0 || nextTile.x >= mapWidth || nextTile.y < 0 || nextTile.y >= mapHeight) {
                continue;
            }

            if(CreatePath(nextTile, pathLength - 1, visited, pathColor, false)) {
                map[currentIdx].r = pathTileColor.r;
                map[currentIdx].g = pathTileColor.g;
                map[currentIdx].b = pathTileColor.b;
                map[currentIdx].a = pathTileColor.a;

                //Debug.Log("Path of color: (" + pathColor.r +", " + pathColor.g +", " + pathColor.b + "): x: " + tile.x + " y:" + tile.y);
                return true;
            }
        }

        //Debug.Log("Returning from x: " + tile.x + ", y: " + tile.y);
        visited[currentIdx] = false;
        return false;
    }

    void AddObstacles() {
        List<Coord> grassTiles = new List<Coord>();
        int numGrassTiles = 0;
        Coord c;
        int currentIdx;

        for(int x = 0; x < mapWidth; x++) {
            for(int y = 0; y < mapHeight; y++) {
                currentIdx = (y * mapWidth) + x;
                if(map[currentIdx].r == grassTileColor.r &&
                    map[currentIdx].g == grassTileColor.g &&
                    map[currentIdx].b == grassTileColor.b &&
                    map[currentIdx].a == grassTileColor.a) {

                    numGrassTiles++;
                    c = new Coord(x, y);
                    grassTiles.Add(c);
                }
            }
        }

        Queue<Coord> grassQ = gen.RandomShuffleArray<Coord>(grassTiles.ToArray());

        for(int i = 1; i <= numGrassTiles * percentObstacles; i++) {
            c = grassQ.Dequeue();
            currentIdx = (c.y * mapWidth) + c.x;

            map[currentIdx].r = obstacleTileColor.r;
            map[currentIdx].g = obstacleTileColor.g;
            map[currentIdx].b = obstacleTileColor.b;
            map[currentIdx].a = obstacleTileColor.a;
        }
    }
}
