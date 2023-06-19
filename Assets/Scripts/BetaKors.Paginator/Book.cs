using System.Collections.Generic;
using System.Linq;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator
{
    [CreateAssetMenu(fileName = "Book", menuName = "Paginator/Book")]
    public sealed class Book : ScriptableObject
    {
        [SerializeField]
        private List<GameObject> pages;

        public List<Page> Pages => _pages;

        public string Name => name;

        private List<Page> _pages = new();

        private Paginator Paginator => Paginator.Instance;

        public void Init()
        {
            foreach (var prefab in pages)
            {
                var obj = Instantiate(
                    prefab,
                    Paginator.PagesPosition,
                    Quaternion.identity,
                    Paginator.Canvas.transform
                );

                _pages.Add(new Page(obj, this));
            }
        }

        public Page FindPageByName(string pageName)
        {
            return Pages.First(page => page.Name.ToLower().StartsWith(pageName.ToLower()));
        }

        public bool IsValid()
        {
            return !Pages.IsEmpty();
        }
    }
}
