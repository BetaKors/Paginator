using System.Collections.Generic;
using System.Linq;
using BetaKors.Extensions;
using UnityEngine;

namespace BetaKors.Paginator
{
    [CreateAssetMenu(fileName = "Book", menuName = "Paginator/Book")]
    public sealed class Book : ScriptableObject
    {
        public List<Page> Pages { get; private set; } = new();

        public string Name => name;

        [SerializeField]
        private List<GameObject> pages;

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

                Pages.Add(new Page(obj, this));

                prefab.SetActive(false);
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
