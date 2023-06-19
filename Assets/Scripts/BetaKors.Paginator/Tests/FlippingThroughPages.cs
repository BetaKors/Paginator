using BetaKors.Animation;
using BetaKors.Paginator.Transitions;
using UnityEngine;

namespace BetaKors.Paginator.Tests
{
    public class FlippingThroughPages : MonoBehaviour
    {
        private Paginator Paginator => Paginator.Instance;

        private int index;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K) && !Paginator.IsMidTransition)
            {
                index = (index + 1) % Paginator.Pages.Count;

                var page = Paginator.Pages[index];

                var transition = new SwipeTransition
                {
                    EasingFunction = EasingFunctions.CubicInOut,
                    Direction = (SwipeDirection)index,
                    Duration = 0.3f,
                };

                StartCoroutine(
                    page.TransitionTo(transition)
                );
            }
        }
    }
}
