using System;
using System.Collections;
using BetaKors.Animation;
using BetaKors.Core;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator.Transitions
{
    public sealed class SlideTransition : Transition
    {
        public Func<Direction, Vector3> CurrentPageStartingPositionFactory { get; set; } = direction =>
        {
            var pos = Paginator.PagesPosition;

            return direction switch
            {
                Direction.Right => pos.WithX(pos.x * 3.0F),
                Direction.Left => pos.NegateX(),
                Direction.Up => pos.WithY(pos.y * 3.0F),
                Direction.Down => pos.NegateY(),
                _ => Vector3.zero
            };
        };

        public Func<Direction, Vector3> PreviousPageTargetPositionFactory { get; set; } = direction =>
        {
            var pos = Paginator.PagesPosition;

            /* just the opposite of the switch above.
               there's probably a better way to implement this. */
            return direction switch
            {
                Direction.Right => pos.NegateX(),
                Direction.Left => pos.WithX(pos.x * 3.0F),
                Direction.Up => pos.NegateY(),
                Direction.Down => pos.WithY(pos.y * 3.0F),
                _ => Vector3.zero
            };
        };

        public Direction Direction { get; set; } = Direction.Right;

        public float Duration { get; set; } = 1.0F;

        public override IEnumerator Execute()
        {
            var pos = Paginator.PagesPosition;

            Animate.Position(
                Paginator.CurrentPage.Transform,
                CurrentPageStartingPositionFactory(Direction),
                Paginator.PagesPosition,
                Duration,
                EasingFunction
            ).StartCoroutine(Paginator);

            Animate.Position(
                Paginator.PreviousPage.Transform,
                Paginator.PagesPosition,
                PreviousPageTargetPositionFactory(Direction),
                Duration,
                EasingFunction
            ).StartCoroutine(Paginator);

            yield return new WaitForSeconds(Duration);

            Paginator.PreviousPage.Transform.position = Paginator.PagesPosition;
        }
    }
}
