using System.Collections;
using System.Reflection;
using BetaKors.Extensions;
using BetaKors.Paginator.Transitions;
using UnityEngine;

namespace BetaKors.Paginator
{
    public sealed class Page
    {
        public GameObject Root { get; private set; }

        public Transform Transform => Root.transform;
        public RectTransform RectTransform => Transform as RectTransform;

        public CanvasGroup CanvasGroup { get; private set; }

        public Book Book { get; private set; }

        public string Name { get; private set; }

        public bool Active
        {
            get => Root.activeInHierarchy;
            set => Root.SetActive(value);
        }

        public bool Interactable
        {
            get => CanvasGroup.interactable;
            set => CanvasGroup.interactable = value;
        }

        private Paginator Paginator => Paginator.Instance;

        public Page(GameObject root, Book book) : this(root, book, root.name) { }

        public Page(GameObject root, Book book, string name)
        {
            Root = root;
            CanvasGroup = Root.GetComponent<CanvasGroup>();
            Book = book;
            Name = name;
        }

        public IEnumerator TransitionTo(Transition transition = null)
        {
            if (this == Paginator.CurrentPage) yield break;

            yield return new WaitUntil(() => !Paginator.IsMidTransition);

            Paginator.IsMidTransition = true;

            Paginator.PreviousPage = Paginator.CurrentPage;
            Paginator.CurrentPage = this;

            Paginator.PreviousPage?.SetInteractable(false);

            Interactable = false;
            Active = true;

            if (transition is not null)
            {
                HandlePageOrdering(transition);
                yield return transition.Execute();
            }

            Paginator.PreviousPage?.SetInteractable(true);
            Paginator.PreviousPage?.SetActive(false);

            Interactable = true;

            Paginator.IsMidTransition = false;
        }

        private void HandlePageOrdering(Transition transition)
        {
            var isCurrentAbovePrevious = transition.PageOrdering is TransitionPageOrdering.CurrentAbovePrevious;

            var pageAbove = isCurrentAbovePrevious ? Paginator.CurrentPage : Paginator.PreviousPage;
            var pageBelow = isCurrentAbovePrevious ? Paginator.PreviousPage : Paginator.CurrentPage;

            pageAbove?.Transform.SetAsFirstSibling();
            pageBelow?.Transform.SetAsFirstSibling();
        }

        public void SetActive(bool value)
        {
            Active = value;
        }

        public void SetInteractable(bool value)
        {
            Interactable = value;
        }
    }
}
