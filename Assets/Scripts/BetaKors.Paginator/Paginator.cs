using System.Collections.Generic;
using System.Linq;
using BetaKors.Animation;
using BetaKors.Core;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator
{
    public sealed class Paginator : Singleton<Paginator>
    {
        [field: SerializeField]
        public List<Book> Library { get; set; }

        public List<Page> Pages => Library.SelectMany(b => b.Pages).ToList();

        public Vector3 PagesPosition => Canvas.transform.position;

        public Page PreviousPage { get; internal set; }
        public Page CurrentPage { get; internal set; }
        public Book CurrentBook => CurrentPage.Book;

        public Canvas Canvas { get; private set; }

        public bool IsMidTransition { get; internal set; }

        private List<Page> flattened;
        private int index = 1;

        void Start()
        {
            HandleSingleton();

            if (Library.IsEmpty())
            {
                return;
            }

            Canvas = FindObjectOfType<Canvas>();

            Library.ForEach(book => book.Init());

            var page = Library.First(book => book.IsValid()).Pages.First();

            StartCoroutine(page.TransitionTo());

            flattened = Library.SelectMany(b => b.Pages).ToList();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K) && !IsMidTransition)
            {
                var page = flattened[index++ % flattened.Count];

                var parameters = new SwipeTransitionParams
                {
                    Direction = SwipeDirection.Up,
                    EasingFunction = EasingFunction.BounceOut,
                    Duration = 0.3f
                };

                var coro = page.TransitionTo(parameters);

                StartCoroutine(coro);
            }
        }

        public Book FindBookByName(string name)
        {
            return Library.First(book => book.Name.ToLower().StartsWith(name.ToLower()));
        }

        public Page FindPageByName(string name)
        {
            return Pages.First(page => page.Name.ToLower().StartsWith(name.ToLower()));
        }
    }
}
