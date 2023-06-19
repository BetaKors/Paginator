using System.Collections;
using BetaKors.Animation;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator
{
    public class TransitionAnimationHandler
    {
        private Paginator Paginator => Paginator.Instance;

        public TransitionAnimationHandler() { }

        private IEnumerator EnlargeTransition(EnlargeParams parameters)
        {
            var pTransform = Paginator.PreviousPage.Root.transform;
            var cTransform = Paginator.CurrentPage.Root.transform;

            cTransform.localScale = Vector3.zero;

            var smallEnlarge = Animate.Scale(pTransform, Vector3.one, Vector3.one * parameters.SmallEnlargeAmount, parameters.SmallEnlargeTime);
            var shrink = Animate.Scale(pTransform, Vector3.one, Vector3.zero, parameters.ShrinkTime);
            var bigEnlarge = Animate.Scale(cTransform, Vector3.zero, Vector3.one, parameters.BigEnlargeTime);

            yield return smallEnlarge.StartCoroutine(Paginator);
            yield return shrink.StartCoroutine(Paginator);
            yield return bigEnlarge.StartCoroutine(Paginator);

            pTransform.localScale = Vector3.one;
        }

        private IEnumerator CrossfadeTransition(CrossfadeParams parameters)
        {
            Animate.Alpha(Paginator.PreviousPage.CanvasGroup, 1.0F, 0F, parameters.Duration).StartCoroutine(Paginator);
            Animate.Alpha(Paginator.CurrentPage.CanvasGroup, 0F, 1.0F, parameters.Duration).StartCoroutine(Paginator);

            yield return new WaitForSeconds(parameters.Duration);

            Paginator.PreviousPage.CanvasGroup.alpha = 1.0F;
        }

        private IEnumerator SwipeLeftTransition(SwipeLeftParams parameters)
        {
            yield return Animate.Position(
                Paginator.CurrentPage.Root.transform,
                Paginator.PagesPosition.WithX(Screen.width * 2F),
                Paginator.PagesPosition,
                parameters.Duration
            ).StartCoroutine(Paginator);
        }
    }
}
