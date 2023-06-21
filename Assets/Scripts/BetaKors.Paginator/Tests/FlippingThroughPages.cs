using BetaKors.Animation;
using BetaKors.Core;
using BetaKors.Extensions;
using BetaKors.Paginator.Transitions;
using UnityEngine;

namespace BetaKors.Paginator.Tests
{
    internal class FlippingThroughPages : MonoBehaviour
    {
        private Paginator Paginator => Paginator.Instance;

        private int _index;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K) && !Paginator.IsMidTransition)
            {
                _index = (_index + 1) % Paginator.Pages.Count;

                var page = Paginator.Pages[_index];

                var transition = new SlideTransition
                {
                    Direction = Utils.RandomFromEnum<Direction>(),
                    EasingFunction = EasingFunctions.BounceOut,
                    Duration = 0.2F,
                };

                page.TransitionTo(transition).StartCoroutine(this);
            }
        }
    }
}
