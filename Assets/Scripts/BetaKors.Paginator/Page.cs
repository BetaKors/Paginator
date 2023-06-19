using System.Collections;
using BetaKors.Extensions;
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
        private TransitionAnimationHandler transitionHandler = new();

        public Page(GameObject root, Book book) : this(root, book, root.name) { }

        public Page(GameObject root, Book book, string name)
        {
            Root = root;
            CanvasGroup = Root.GetComponent<CanvasGroup>();
            Book = book;
            Name = name;
            Root.SetActive(false);
        }

        public IEnumerator TransitionTo()
        {
            yield return TransitionTo(null);
        }

        public IEnumerator TransitionTo(TransitionParams transitionParams)
        {
            if (this == Paginator.CurrentPage) yield break;

            yield return new WaitUntil(() => !Paginator.IsMidTransition);

            Paginator.IsMidTransition = true;

            Paginator.PreviousPage = Paginator.CurrentPage;
            Paginator.CurrentPage = this;

            Paginator.CurrentPage.Transform.SetAsFirstSibling();

            if (Paginator.PreviousPage is not null)
            {
                Paginator.PreviousPage.Transform.SetAsFirstSibling();
                Paginator.PreviousPage.Interactable = false;
            }

            Interactable = false;

            Active = true;

            if (transitionParams is not null)
            {
                var methodName = transitionParams.GetType().Name.Replace("TransitionParams", "");
                yield return transitionHandler.InvokeMethod(methodName, transitionParams);
            }

            if (Paginator.PreviousPage is not null)
            {
                Paginator.PreviousPage.Interactable = true;
                Paginator.PreviousPage.Active = false;
            }

            Interactable = true;

            Paginator.IsMidTransition = false;
        }
    }
}
