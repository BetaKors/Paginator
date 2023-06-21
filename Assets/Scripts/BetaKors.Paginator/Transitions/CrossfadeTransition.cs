using System.Collections;
using BetaKors.Animation;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator.Transitions
{
    public sealed class CrossfadeTransition : Transition
    {
        public float Duration { get; set; } = 1.0F;

        public override IEnumerator Execute()
        {
            Animate.Alpha(
                Paginator.PreviousPage.CanvasGroup,
                1.0F,
                0F,
                Duration,
                EasingFunction
            ).StartCoroutine(Paginator);

            Animate.Alpha(
                Paginator.CurrentPage.CanvasGroup,
                0F,
                1.0F,
                Duration,
                EasingFunction
            ).StartCoroutine(Paginator);

            yield return new WaitForSeconds(Duration);

            Paginator.PreviousPage.CanvasGroup.alpha = 1.0F;
        }
    }
}
