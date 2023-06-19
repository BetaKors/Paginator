using System;
using UnityEngine;

namespace BetaKors.Animation
{
    public static class EasingFunctions
    {
        public static Func<float, float> Linear => x => x;

        public static Func<float, float> CubicInOut => x => x < 0.5F ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
        public static Func<float, float> CubicOut => x => x * x * x;
        public static Func<float, float> CubicIn => x => 1 - Mathf.Pow(1 - x, 3);

        public static Func<float, float> ElasticInOut => x =>
        {

            const float c5 = (2 * Mathf.PI) / 4.5F;

            return x == 0
            ? 0
            : x == 1
            ? 1
            : x < 0.5F
            ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125F) * c5)) / 2
            : Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125F) * c5) / 2 + 1;
        };

        public static Func<float, float> BounceOut => x =>
        {
            const float n1 = 7.5625F;
            const float d1 = 2.75F;

            if (x < 1F / d1)
            {
                return n1 * x * x;
            }
            else if (x < 2F / d1)
            {
                return n1 * (x -= 1.5F / d1) * x + 0.75F;
            }
            else if (x < 2.5F / d1)
            {
                return n1 * (x -= 2.25F / d1) * x + 0.9375F;
            }
            else
            {
                return n1 * (x -= 2.625F / d1) * x + 0.984375F;
            }
        };

        public static Func<float, float> BounceIn => x => 1 - BounceOut(1 - x);
    }

    public enum EasingFunction
    {
        Linear,
        CubicInOut,
        CubicOut,
        CubicIn,
        BounceOut,
        BounceIn,
        ElasticInOut,
    }
}
