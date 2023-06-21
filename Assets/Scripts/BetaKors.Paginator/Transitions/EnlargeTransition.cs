using System.Collections;
using BetaKors.Animation;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator.Transitions
{
    public sealed class EnlargeTransition : Transition
    {
        public float SmallEnlargeAmount { get; set; } = 1.1F;
        public float SmallEnlargeTime { get; set; } = 0.03F;
        public float ShrinkTime { get; set; } = 0.08F;
        public float BigEnlargeTime { get; set; } = 0.12F;

        internal override IEnumerator Execute()
        {
            var pTransform = Paginator.PreviousPage.Transform;
            var cTransform = Paginator.CurrentPage.Transform;

            cTransform.localScale = Vector3.zero;

            var smallEnlarge = Animate.Scale(
                pTransform,
                Vector3.one,
                Vector3.one * SmallEnlargeAmount,
                SmallEnlargeTime,
                EasingFunction
            );

            var shrink = Animate.Scale(
                pTransform,
                Vector3.one,
                Vector3.zero,
                ShrinkTime,
                EasingFunction
            );

            var bigEnlarge = Animate.Scale(
                cTransform,
                Vector3.zero,
                Vector3.one,
                BigEnlargeTime,
                EasingFunction
            );

            yield return smallEnlarge.StartCoroutine(Paginator);
            yield return shrink.StartCoroutine(Paginator);
            yield return bigEnlarge.StartCoroutine(Paginator);

            pTransform.localScale = Vector3.one;
        }
    }
}
