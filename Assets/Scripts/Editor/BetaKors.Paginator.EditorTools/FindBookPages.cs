using System.IO;
using UnityEditor;
using UnityEngine;

namespace BetaKors.Paginator.EditorTools
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Book))]
    public class FindBookPages : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!GUILayout.Button("Find pages in this book's folder"))
            {
                return;
            }

            foreach (var target in targets)
            {
                FindPages(target as Book);
            }
        }

        private void FindPages(Book book)
        {
            var path = AssetDatabase.GetAssetPath(book);
            var directory = Path.GetDirectoryName(path);

            var prefabsGUIDs = AssetDatabase.FindAssets("t:prefab", new[] { directory });

            foreach (var guid in prefabsGUIDs)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

                if (!book.GameObjects.Contains(prefab))
                {
                    book.GameObjects.Add(prefab);
                }
            }
        }
    }
}
