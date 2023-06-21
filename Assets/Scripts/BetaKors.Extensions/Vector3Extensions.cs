using UnityEngine;

namespace BetaKors.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 OnlyX(this Vector3 v) => new(v.x, 0f, 0f);

        public static Vector3 OnlyY(this Vector3 v) => new(0f, v.y, 0f);

        public static Vector3 OnlyZ(this Vector3 v) => new(0f, 0f, v.z);

        public static Vector3 NegateX(this Vector3 v) => new(-v.x, v.y, v.z);

        public static Vector3 NegateY(this Vector3 v) => new(v.x, -v.y, v.z);

        public static Vector3 NegateZ(this Vector3 v) => new(v.x, v.y, -v.z);

        public static Vector3 WithX(this Vector3 v, float x) => new(x, v.y, v.z);

        public static Vector3 WithY(this Vector3 v, float y) => new(v.x, y, v.z);

        public static Vector3 WithZ(this Vector3 v, float z) => new(v.x, v.y, z);
    }
}