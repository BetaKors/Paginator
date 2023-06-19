using System.Collections;
using BetaKors.Animation;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator.Transitions
{
    public class TransitionHandler
    {
        private static Paginator Paginator => Paginator.Instance;

        private static IEnumerator Enlarge(EnlargeTransition parameters)
        {
            var pTransform = Paginator.PreviousPage.Transform;
            var cTransform = Paginator.CurrentPage.Transform;

            cTransform.localScale = Vector3.zero;

            var smallEnlarge = Animate.Scale(
                pTransform,
                Vector3.one,
                Vector3.one * parameters.SmallEnlargeAmount,
                parameters.SmallEnlargeTime,
                parameters.EasingFunction
            );

            var shrink = Animate.Scale(
                pTransform,
                Vector3.one,
                Vector3.zero,
                parameters.ShrinkTime,
                parameters.EasingFunction
            );

            var bigEnlarge = Animate.Scale(
                cTransform,
                Vector3.zero,
                Vector3.one,
                parameters.BigEnlargeTime,
                parameters.EasingFunction
            );

            yield return smallEnlarge.StartCoroutine(Paginator);
            yield return shrink.StartCoroutine(Paginator);
            yield return bigEnlarge.StartCoroutine(Paginator);

            pTransform.localScale = Vector3.one;
        }

        private static IEnumerator Crossfade(CrossfadeTransition parameters)
        {
            Animate.Alpha(
                Paginator.PreviousPage.CanvasGroup,
                1.0F,
                0F,
                parameters.Duration,
                parameters.EasingFunction
            ).StartCoroutine(Paginator);

            Animate.Alpha(
                Paginator.CurrentPage.CanvasGroup,
                0F,
                1.0F,
                parameters.Duration,
                parameters.EasingFunction
            ).StartCoroutine(Paginator);

            yield return new WaitForSeconds(parameters.Duration);

            Paginator.PreviousPage.CanvasGroup.alpha = 1.0F;
        }

        private static IEnumerator Swipe(SwipeTransition parameters)
        {
            var x = Paginator.CurrentPage.RectTransform.rect.xMax;
            var y = Paginator.CurrentPage.RectTransform.rect.yMax;

            var startPosition = parameters.Direction switch
            {
                SwipeDirection.Right => Paginator.PagesPosition.WithX(-x),
                SwipeDirection.Left => Paginator.PagesPosition.WithX(x * 3F),
                SwipeDirection.Up => Paginator.PagesPosition.WithY(-y),
                SwipeDirection.Down => Paginator.PagesPosition.WithY(y * 3F),
                SwipeDirection.Custom => parameters.StartingPosition,
                _ => Vector3.zero
            };

            yield return Animate.Position(
                Paginator.CurrentPage.Transform,
                startPosition,
                Paginator.PagesPosition,
                parameters.Duration,
                parameters.EasingFunction
            ).StartCoroutine(Paginator);
        }
    }
}
