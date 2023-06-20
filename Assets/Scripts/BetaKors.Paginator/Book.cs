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

        [field: SerializeField]
        public List<GameObject> GameObjects { get; private set; } = new();

        public string Name => name;

        private Paginator Paginator => Paginator.Instance;

        public void Init()
        {
            foreach (var prefab in GameObjects)
            {
                var obj = Instantiate(
                    prefab,
                    Paginator.PagesPosition,
                    Quaternion.identity,
                    Paginator.Canvas.transform
                );

                AddPage(obj);

                prefab.SetActive(false);
            }
        }

        public Page AddPage(GameObject obj)
        {
            var page = new Page(obj, this);
            Pages.Add(page);
            return page;
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
