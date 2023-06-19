using System.Collections;
using BetaKors.Animation;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator
{
    public sealed class Page
    {
        public GameObject Root { get; private set; }
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
            yield return TransitionTo(TransitionType.None, null);
        }

        public IEnumerator TransitionTo(TransitionType animationType, TransitionParams parameters)
        {
            if (this == Paginator.CurrentPage) yield break;

            yield return new WaitUntil(() => !Paginator.IsMidTransition);

            Paginator.IsMidTransition = true;

            Paginator.PreviousPage = Paginator.CurrentPage;
            Paginator.CurrentPage = this;

            if (Paginator.PreviousPage is not null)
            {
                Paginator.PreviousPage.Interactable = false;
            }

            Interactable = false;

            Active = true;

            if (animationType is not TransitionType.None)
            {
                var name = System.Enum.GetName(typeof(TransitionType), animationType);
                yield return transitionHandler.InvokeMethod($"{name}Transition", parameters);
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
