using System;
using System.Collections;
using BetaKors.Animation;
using BetaKors.Core;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator.Transitions
{
    public sealed class SwipeTransition : Transition
    {
        public Func<Direction, Vector3> StartingPositionFactory { get; set; } = direction =>
        {
            var pos = Paginator.PagesPosition;

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

        internal override IEnumerator Execute()
        {
            yield return Animate.Position(
                Paginator.CurrentPage.Transform,
                StartingPositionFactory(Direction),
                Paginator.PagesPosition,
                Duration,
                EasingFunction
            ).StartCoroutine(Paginator);
        }
    }
}
