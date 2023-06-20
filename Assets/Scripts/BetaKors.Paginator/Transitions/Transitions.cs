using System;
using BetaKors.Animation;
using BetaKors.Core;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator.Transitions
{
    public abstract class Transition
    {
        public Func<float, float> EasingFunction { get; set; } = EasingFunctions.Linear;

        public virtual string Name => GetType().Name.Remove("Transition");

        protected internal virtual TransitionType Type => TransitionType.CurrentAbovePrevious;

        protected static Paginator Paginator => Paginator.Instance;
    }

    public abstract class TransitionWithDuration : Transition
    {
        public float Duration { get; set; }
    }

    public sealed class CrossfadeTransition : TransitionWithDuration { }

    public sealed class SlideTransition : TransitionWithDuration
    {
        public Direction Direction { get; set; } = Direction.Right;
    }

    public sealed class SwitchWindowsTransition : TransitionWithDuration
    {
        protected internal override TransitionType Type => TransitionType.PreviousAboveCurrent;

        public Func<Vector3> TargetPositionFactory { get; set; } = () =>
        {
            var rect = Paginator.CurrentPage.RectTransform.rect;
            var x = rect.xMax;
            var y = 0F;
            return new(x, y, 0F);
        };
    }

    public sealed class EnlargeTransition : Transition
    {
        public float SmallEnlargeAmount { get; set; } = 1.1F;
        public float SmallEnlargeTime { get; set; } = 0.03F;
        public float ShrinkTime { get; set; } = 0.08F;
        public float BigEnlargeTime { get; set; } = 0.12F;
    }

    public sealed class SwipeTransition : TransitionWithDuration
    {
        public Direction Direction { get; set; } = Direction.Right;

        public Func<Direction, Vector3> StartingPositionFactory { get; set; } = direction =>
        {
            var x = Paginator.CurrentPage.RectTransform.rect.xMax;
            var y = Paginator.CurrentPage.RectTransform.rect.yMax;

            return direction switch
            {
                Direction.Right => Paginator.PagesPosition.WithX(-x),
                Direction.Left => Paginator.PagesPosition.WithX(x * 3F),
                Direction.Up => Paginator.PagesPosition.WithY(-y),
                Direction.Down => Paginator.PagesPosition.WithY(y * 3F),
                _ => Vector3.zero
            };
        };
    }
}
