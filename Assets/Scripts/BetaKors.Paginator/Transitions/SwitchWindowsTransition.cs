using System;
using System.Collections;
using BetaKors.Animation;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator.Transitions
{
    public sealed class SwitchWindowsTransition : Transition
    {
        public float Duration { get; set; } = 1.0F;

        public Func<Vector3> TargetPositionFactory { get; set; } = () =>
        {
            var rect = Paginator.CurrentPage.RectTransform.rect;
            var x = rect.xMax;
            var y = 0F;
            return new(x, y, 0F);
        };

        protected internal override TransitionPageOrdering PageOrdering => TransitionPageOrdering.PreviousAboveCurrent;

        internal override IEnumerator Execute()
        {
            Animate.Position(
                Paginator.PreviousPage.Transform,
                Paginator.PreviousPage.Transform.position,
                TargetPositionFactory(),
                Duration,
                EasingFunction
            ).StartCoroutine(Paginator);

            Animate.Scale(
                Paginator.PreviousPage.Transform,
                Vector3.one,
                Vector3.zero,
                Duration,
                EasingFunction
            ).StartCoroutine(Paginator);

            yield return new WaitForSeconds(Duration);

            Paginator.PreviousPage.Transform.position = Paginator.PagesPosition;
            Paginator.PreviousPage.Transform.localScale = Vector3.one;
        }
    }
}
