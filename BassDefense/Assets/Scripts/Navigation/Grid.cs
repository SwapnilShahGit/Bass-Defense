using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public LayerMask unwalkableMask;
    public LayerMask aiUnwalkableMask;
    public LayerMask pathMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    public void StartCreatingGrid() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public int MaxSize {
        get {
            return gridSizeX * gridSizeY;
        }
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

        for(int x = 0; x < gridSizeX; x++) {
            for(int y = 0; y < gridSizeY; y++) {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);

                // if Path walkable by all
                if((Physics2D.OverlapCircle(worldPoint, nodeRadius, pathMask.value) != null)) {
                    grid[x, y] = new Node(true, true, worldPoint, x, y);
                }
                // if forest
                else if((Physics2D.OverlapCircle(worldPoint, nodeRadius, aiUnwalkableMask.value) != null)) {
                    grid[x, y] = new Node(true, false, worldPoint, x, y);
                }
                else {
                    grid[x, y] = new Node(false, false, worldPoint, x, y);
                }
                //bool walkable = ((Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask.value) == null));
                //bool aiWalkable = ((Physics2D.OverlapCircle(worldPoint, nodeRadius, aiUnwalkableMask.value) == null));
            }
        }
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();
        for(int x = -1; x <= 1; x++) {
            for(int y = -1; y <= 1; y++) {
                if(x == 0 && y == 0) {
                    continue;
                }

                int checkX = node.x + x;
                int checkY = node.y + y;

                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition) {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if(grid != null) {
            foreach(Node n in grid) {
                if(!n.walkable) {
                    Gizmos.color = Color.red;
                }
                else if(!n.aiWalkable) {
                    Gizmos.color = Color.yellow;
                }
                else {
                    Gizmos.color = Color.green;
                }
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
