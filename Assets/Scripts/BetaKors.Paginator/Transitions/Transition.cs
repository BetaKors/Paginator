using System;
using System.Collections;
using BetaKors.Animation;

namespace BetaKors.Paginator.Transitions
{
    public abstract class Transition
    {
        public Func<float, float> EasingFunction { get; set; } = EasingFunctions.Linear;

        protected internal virtual TransitionPageOrdering PageOrdering => TransitionPageOrdering.CurrentAbovePrevious;

        protected static Paginator Paginator => Paginator.Instance;

        public abstract IEnumerator Execute();
    }
}
