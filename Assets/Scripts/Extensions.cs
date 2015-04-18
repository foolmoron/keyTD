using UnityEngine;
using System.Collections;

public static class Extensions {

    public static Color withAlpha(this Color color, float alpha) {
        color.a = alpha;
        return color;
    }
}
