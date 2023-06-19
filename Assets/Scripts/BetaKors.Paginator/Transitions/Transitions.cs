using System;
using BetaKors.Animation;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator.Transitions
{
    public class Transition
    {
        public Func<float, float> EasingFunction { get; set; } = EasingFunctions.Linear;

        public virtual string Name => GetType().Name.Remove("Transition");
    }

    public abstract class TransitionWithDuration : Transition
    {
        public float Duration { get; set; }
    }

    public sealed class CrossfadeTransition : TransitionWithDuration { }

    public sealed class EnlargeTransition : Transition
    {
        public float SmallEnlargeAmount { get; set; }
        public float SmallEnlargeTime { get; set; }
        public float ShrinkTime { get; set; }
        public float BigEnlargeTime { get; set; }
    }

    public sealed class SwipeTransition : TransitionWithDuration
    {
        public SwipeDirection Direction { get; set; }

        public Vector3 StartingPosition { get; set; }
    }
}
