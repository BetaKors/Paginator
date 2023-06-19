using BetaKors.Animation;
using UnityEngine;

namespace BetaKors.Paginator
{
    public class TransitionParams
    {
        public EasingFunction EasingFunction { get; set; }
    }

    public abstract class TransitionWithDurationParams : TransitionParams
    {
        public float Duration { get; set; }
    }

    public sealed class EnlargeTransitionParams : TransitionParams
    {
        public float SmallEnlargeAmount { get; set; }
        public float SmallEnlargeTime { get; set; }
        public float ShrinkTime { get; set; }
        public float BigEnlargeTime { get; set; }
    }

    public sealed class CrossfadeTransitionParams : TransitionWithDurationParams { }

    public sealed class SwipeTransitionParams : TransitionWithDurationParams
    {
        public SwipeDirection Direction { get; set; }
        public Vector3 StartingPosition { get; set; }
    }
}
