using System;
using UnityEngine;

namespace BetaKors.Animation
{
    /* from: https://easings.net */
    public static class EasingFunctions
    {
        public static Func<float, float> Linear => x => x;

        public static Func<float, float> CubicInOut => x => x < 0.5F ? 4F * x * x * x : 1F - Mathf.Pow(-2F * x + 2F, 3F) / 2F;
        public static Func<float, float> CubicOut => x => x * x * x;
        public static Func<float, float> CubicIn => x => 1F - Mathf.Pow(1F - x, 3F);

        public static Func<float, float> QuadInOut => x => x < 0.5F ? 2 * x * x : 1F - Mathf.Pow(-2F * x + 2F, 2F) / 2F;
        public static Func<float, float> QuadOut => x => 1F - (1F - x) * (1F - x);
        public static Func<float, float> QuadIn => x => x * x;

        public static Func<float, float> QuintInOut => x => x < 0.5F ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
        public static Func<float, float> QuintOut => x => 1F - Mathf.Pow(1F - x, 5F);
        public static Func<float, float> QuintIn => x => x * x * x * x * x;

        public static Func<float, float> ExpoInOut => x =>
        {
            return x == 0F
            ? 0F
            : x == 1F
            ? 1F
            : x < 0.5F ? Mathf.Pow(2F, 20F * x - 10F) / 2F
            : (2F - Mathf.Pow(2F, -20F * x + 10F)) / 2F;
        };

        public static Func<float, float> ExpoOut => x => x == 1F ? 1F : 1F - Mathf.Pow(2F, -10F * x);
        public static Func<float, float> ExpoIn => x => x == 0F ? 0F : Mathf.Pow(2F, 10F * x - 10F);

        public static Func<float, float> BounceInOut => x =>
        {
            return x < 0.5F
            ? (1F - BounceOut(1F - 2F * x)) / 2F
            : (1F + BounceOut(2F * x - 1F)) / 2F;
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

        public static Func<float, float> BounceIn => x => 1F - BounceOut(1F - x);

        public static Func<float, float> ElasticOut => x =>
        {
            const float c4 = 2F * Mathf.PI / 3F;

            return x == 0F
            ? 0F
            : x == 1F
            ? 1F
            : Mathf.Pow(2F, -10F * x) * Mathf.Sin((x * 10F - 0.75F) * c4) + 1F;
        };
    }
}
