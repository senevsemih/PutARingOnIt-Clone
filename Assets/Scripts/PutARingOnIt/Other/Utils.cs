using UnityEngine;

namespace PutARingOnIt.Other
{
    public static class Utils
    {
        public static float GetRandomValueAsRange(this Vector2 v2) => Random.Range(v2.x, v2.y);
    }
}