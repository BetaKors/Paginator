using System.IO;
using UnityEditor;
using UnityEngine;

namespace BetaKors.Paginator.EditorTools
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Book))]
    public class FindBookPagesInFolder : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Load pages from this book into the Canvas"))
            {
                var canvas = FindObjectOfType<Canvas>().transform;

                foreach (var target in targets)
                {
                    LoadPagesIntoCanvas(target as Book, canvas);
                }
            }
            else if (GUILayout.Button("Find pages in this book's folder"))
            {
                foreach (var target in targets)
                {
                    FindPages(target as Book);
                }
            }
        }

        private void LoadPagesIntoCanvas(Book book, Transform canvas)
        {
            Undo.IncrementCurrentGroup();

            Undo.SetCurrentGroupName("Paginator");

            foreach (var prefab in book.GameObjects)
            {
                var instantiated = PrefabUtility.InstantiatePrefab(prefab, canvas) as GameObject;
                instantiated.transform.position = canvas.position;
                instantiated.SetActive(false);

                Undo.RegisterCreatedObjectUndo(instantiated, $"Load page {prefab.name} from book {book.name}");
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
