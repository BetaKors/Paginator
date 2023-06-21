using UnityEngine;

namespace BetaKors.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 OnlyX(this Vector2 v) => new(v.x, 0f);

        public static Vector2 OnlyY(this Vector2 v) => new(0f, v.y);

        public static Vector2 NegateX(this Vector2 v) => new(-v.x, v.y);

        public static Vector2 NegateY(this Vector2 v) => new(v.x, -v.y);

        public static Vector2 WithX(this Vector2 v, float x) => new(x, v.y);

        public static Vector2 WithY(this Vector2 v, float y) => new(v.x, y);

        public static Vector3 WithZ(this Vector2 v, float z) => new(v.x, v.y, z);
    }
}
