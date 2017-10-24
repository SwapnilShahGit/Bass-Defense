using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapData {
    public static int Width {
        get {
            return Width;
        }
        set {
            Width = value;
        }
    }

    public static int Height {
        get {
            return Height;
        }
        set {
            Height = value;
        }
    }

    public static Color32[] ColorMap {
        get {
            return ColorMap;
        }
        set {
            ColorMap = value;
        }
    }

    public static float BaseLocationX {
        get {
            return BaseLocationX;
        }
        set {
            BaseLocationX = value;
        }
    }

    public static float BaseLocationY {
        get {
            return BaseLocationY;
        }
        set {
            BaseLocationY = value;
        }
    }
}
