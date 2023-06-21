using System;
using UnityEngine;

namespace BetaKors.Animation
{
    /* from: https://easings.net */
    public static class EasingFunctions
    {
        public static Func<float, float> Linear => x => x;

        public static Func<float, float> CubicInOut => x => x < 0.5F ? 4.0F * x * x * x : 1.0F - Mathf.Pow(-2.0F * x + 2.0F, 3.0F) / 2.0F;
        public static Func<float, float> CubicOut => x => x * x * x;
        public static Func<float, float> CubicIn => x => 1.0F - Mathf.Pow(1.0F - x, 3.0F);

        public static Func<float, float> QuadInOut => x => x < 0.5F ? 2.0F * x * x : 1.0F - Mathf.Pow(-2.0F * x + 2.0F, 2.0F) / 2.0F;
        public static Func<float, float> QuadOut => x => 1.0F - (1.0F - x) * (1.0F - x);
        public static Func<float, float> QuadIn => x => x * x;

        public static Func<float, float> QuintInOut => x => x < 0.5F ? 16.0F * x * x * x * x * x : 1 - Mathf.Pow(-2.0F * x + 2.0F, 5.0F) / 2.0F;
        public static Func<float, float> QuintOut => x => 1.0F - Mathf.Pow(1.0F - x, 5.0F);
        public static Func<float, float> QuintIn => x => x * x * x * x * x;

        public static Func<float, float> ExpoInOut => x =>
        {
            return x == 0.0F
            ? 0.0F
            : x == 1.0F
            ? 1.0F
            : x < 0.5F ? Mathf.Pow(2.0F, 20.0F * x - 10.0F) / 2.0F
            : (2.0F - Mathf.Pow(2.0F, -20.0F * x + 10.0F)) / 2.0F;
        };

        public static Func<float, float> ExpoOut => x => x == 1.0F ? 1.0F : 1.0F - Mathf.Pow(2.0F, -10.0F * x);
        public static Func<float, float> ExpoIn => x => x == 0.0F ? 0.0F : Mathf.Pow(2.0F, 10.0F * x - 10.0F);

        public static Func<float, float> BounceInOut => x =>
        {
            return x < 0.5F
            ? (1.0F - BounceOut(1.0F - 2.0F * x)) / 2.0F
            : (1.0F + BounceOut(2.0F * x - 1.0F)) / 2.0F;
        };

        public static Func<float, float> BounceOut => x =>
        {
            const float n1 = 7.5625F;
            const float d1 = 2.75F;

            if (x < 1.0F / d1)
            {
                return n1 * x * x;
            }
            else if (x < 2.0F / d1)
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

        public static Func<float, float> BounceIn => x => 1.0F - BounceOut(1.0F - x);

        public static Func<float, float> ElasticOut => x =>
        {
            const float c4 = 2.0F * Mathf.PI / 3.0F;

            return x == 0.0F
            ? 0.0F
            : x == 1.0F
            ? 1.0F
            : Mathf.Pow(2.0F, -10.0F * x) * Mathf.Sin((x * 10.0F - 0.75F) * c4) + 1.0F;
        };
    }
}
