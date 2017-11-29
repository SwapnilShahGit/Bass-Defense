using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>{
    public bool walkable;
    public bool aiWalkable;

    public Vector3 worldPosition;

    public int gCost;
    public int hCost;

    public int x;
    public int y;

    public Node parent;

    int heapIndex;

    public Node(bool _walkable, bool _aiWalkable, Vector3 _worldPosition, int _x, int _y) {
        walkable = _walkable;
        aiWalkable = _aiWalkable;
        worldPosition = _worldPosition;
        x = _x;
        y = _y;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }

    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
