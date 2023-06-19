using UnityEngine;

namespace BetaKors
{
    public static class Utils
    {
        // from: https://stackoverflow.com/a/4801397
        public static Color ColorFromHex(string hex)
        {
            int rgb = System.Convert.ToInt32(hex.TrimStart('#'), 16);

            return new(
                ((rgb >> 16) & 0xff) / 255.0F,
                ((rgb >> 8) & 0xff) / 255.0F,
                ((rgb) & 0xff) / 255.0F
            );
        }

        public static float Remap(float n, float start1, float stop1, float start2, float stop2)
        {
            return (n - start1) / (stop1 - start1) * (stop2 - start2) + start2;
        }

        public static Vector3 RandomPositionInsideBounds(Bounds bounds)
        {
            return new(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}