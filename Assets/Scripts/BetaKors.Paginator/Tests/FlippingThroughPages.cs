using BetaKors.Animation;
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
                    EasingFunction = EasingFunctions.CubicIn,
                    Duration = 0.5F,
                };

                StartCoroutine(
                    page.TransitionTo(transition)
                );
            }
        }
    }
}
