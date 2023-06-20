using System.Collections;
using BetaKors.Animation;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator.Transitions
{
    internal static class TransitionHandler
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

        private static IEnumerator Slide(SlideTransition parameters)
        {
            var cRect = Paginator.CurrentPage.RectTransform.rect;
            var pRect = Paginator.CurrentPage.RectTransform.rect;

            var bottomLeftCorner = cRect.min;
            var topLeftCorner = cRect.max;

            Animate.Function(
                t => cRect.min = Vector3.Lerp(bottomLeftCorner, topLeftCorner, t).WithY(cRect.min.y),
                parameters.Duration,
                parameters.EasingFunction
            ).StartCoroutine(Paginator);

            Animate.Function(
                t => pRect.max = Vector3.Lerp(topLeftCorner, bottomLeftCorner, t).WithY(pRect.min.y),
                parameters.Duration,
                parameters.EasingFunction
            ).StartCoroutine(Paginator);

            yield return new WaitForSeconds(parameters.Duration);

            Paginator.PreviousPage.Transform.position = Paginator.PagesPosition;
        }

        private static IEnumerator SwitchWindows(SwitchWindowsTransition parameters)
        {
            Animate.Position(
                Paginator.PreviousPage.Transform,
                Paginator.PreviousPage.Transform.position,
                parameters.TargetPositionFactory(),
                parameters.Duration,
                parameters.EasingFunction
            ).StartCoroutine(Paginator);

            Animate.Scale(
                Paginator.PreviousPage.Transform,
                Vector3.one,
                Vector3.zero,
                parameters.Duration,
                parameters.EasingFunction
            ).StartCoroutine(Paginator);

            yield return new WaitForSeconds(parameters.Duration);

            Paginator.PreviousPage.Transform.position = Paginator.PagesPosition;
            Paginator.PreviousPage.Transform.localScale = Vector3.one;
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
            yield return Animate.Position(
                Paginator.CurrentPage.Transform,
                parameters.StartingPositionFactory(parameters.Direction),
                Paginator.PagesPosition,
                parameters.Duration,
                parameters.EasingFunction
            ).StartCoroutine(Paginator);
        }
    }
}
