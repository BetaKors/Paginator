using System;
using System.Collections;
using System.Reflection;
using BetaKors.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace BetaKors.Animation
{
    public static class Animate
    {
        public static IEnumerator Position(Transform target, Vector3 a, Vector3 b, float duration, EasingFunction easingFunction)
        {
            yield return Function(
                t => target.position = Vector3.Lerp(a, b, t),
                duration,
                easingFunction
            );
        }

        public static IEnumerator Rotation(Transform target, Quaternion a, Quaternion b, float duration, EasingFunction easingFunction)
        {
            yield return Function(
                t => target.rotation = Quaternion.Slerp(a, b, t),
                duration,
                easingFunction
            );
        }

        public static IEnumerator Rotation(Transform target, Vector3 eulerA, Vector3 eulerB, float duration, EasingFunction easingFunction)
        {
            var a = Quaternion.Euler(eulerA);
            var b = Quaternion.Euler(eulerB);
            yield return Rotation(target, a, b, duration, easingFunction);
        }

        public static IEnumerator Scale(Transform target, Vector3 a, Vector3 b, float duration, EasingFunction easingFunction)
        {
            yield return Function(
                t => target.localScale = Vector3.Lerp(a, b, t),
                duration,
                easingFunction
            );
        }

        public static IEnumerator Color(Image target, Color a, Color b, float duration, EasingFunction easingFunction)
        {
            yield return Function(
                t => target.color = UnityEngine.Color.Lerp(a, b, t),
                duration,
                easingFunction
            );
        }

        public static IEnumerator Alpha(Image target, float a, float b, float duration, EasingFunction easingFunction)
        {
            yield return Function(
                t => target.color = target.color.WithAlpha(Mathf.Lerp(a, b, t)),
                duration,
                easingFunction
            );
        }

        public static IEnumerator Alpha(CanvasGroup target, float a, float b, float duration, EasingFunction easingFunction)
        {
            yield return Function(
                t => target.alpha = Mathf.Lerp(a, b, t),
                duration,
                easingFunction
            );
        }

        public static IEnumerator Function(Action<float> function, float duration, EasingFunction easingFunction = EasingFunction.Linear, float startingT = 0f)
        {
            var actualEasingFunc = GetEasingFunction(easingFunction);

            for (float t = startingT; t <= duration; t += Time.deltaTime)
            {
                function(actualEasingFunc(t / duration));
                yield return null;
            }

            /* the for loop above doesn't guarantee that function will be called
               with a value of 1.0F, so i'm ensuring it is going to be here. */
            function(actualEasingFunc(1.0F));
        }

        private static Func<float, float> GetEasingFunction(EasingFunction easingFunction)
        {
            var name = Enum.GetName(easingFunction.GetType(), easingFunction);
            var flags = BindingFlags.Public | BindingFlags.Static;
            var property = typeof(EasingFunctions).GetProperty(name, flags);
            return property.GetValue(null) as Func<float, float>;
        }
    }
}
