using UnityEngine;
using System.Collections;

public static class Extensions {

    public static Color withAlpha(this Color color, float alpha) {
        color.a = alpha;
        return color;
    }

    public static Vector3 withX(this Vector3 vector, float value) {
        vector.x = value;
        return vector;
    }
    public static Vector3 withY(this Vector3 vector, float value) {
        vector.y = value;
        return vector;
    }
    public static Vector3 withZ(this Vector3 vector, float value) {
        vector.z = value;
        return vector;
    }
}
