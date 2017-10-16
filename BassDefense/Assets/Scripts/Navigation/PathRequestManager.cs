using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour {

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;
    Pathfinding pathfinding;

    bool isProcessingPath;

    static PathRequestManager instance;

    void Awake() {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector2 pathStart, Vector2 pathEnd, Action<Vector3[], bool> callback, bool isAI) {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback, isAI);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext() {
        if(!isProcessingPath && pathRequestQueue.Count > 0) {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd, currentPathRequest.isAI);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success) {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest {
        public Vector2 pathStart;
        public Vector2 pathEnd;
        public Action<Vector3[], bool> callback;
        public bool isAI;

        public PathRequest(Vector2 start, Vector2 end, Action<Vector3[], bool> call, bool isAnAI) {
            pathStart = start;
            pathEnd = end;
            callback = call;
            isAI = isAnAI;
        }
    }
}
