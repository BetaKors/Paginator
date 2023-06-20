using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BetaKors.Paginator.EditorTools
{
    public static class CreateBookMenuItem
    {
        [MenuItem("Paginator/Create book from the selected GameObjects")]
        public static void CreateBook()
        {
            var book = ScriptableObject.CreateInstance<Book>();

            var path = EditorUtility.SaveFilePanelInProject("Create book", "Book", "asset", "Create book");

            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            AssetDatabase.CreateAsset(book, path);

            Undo.IncrementCurrentGroup();

            foreach (var obj in Selection.gameObjects)
            {
                var prefab = CreatePrefab(obj, path);

                if (prefab == null)
                {
                    continue;
                }

                if (obj.GetComponent<CanvasGroup>() == null)
                {
                    Debug.Log($"GameObject {obj.name} did not have a CanvasGroup attached to it, and as such, one was added.");
                    Undo.AddComponent<CanvasGroup>(obj);
                }

                Undo.DestroyObjectImmediate(obj);

                book.GameObjects.Add(prefab);
            }

            Undo.SetCurrentGroupName("Paginator");

            EditorUtility.FocusProjectWindow();
        }

        [MenuItem("Paginator/Create book from the selected GameObjects", true)]
        public static bool CreateBookCheck()
        {
            return Selection.gameObjects.All(obj => (
                obj != null &&
                obj.transform.parent != null &&
                obj.transform.parent == Object.FindObjectOfType<Canvas>().transform
            ));
        }

        private static GameObject CreatePrefab(GameObject original, string path)
        {
            var prefab = PrefabUtility.SaveAsPrefabAsset(original, $"{Path.GetDirectoryName(path)}\\{original.name}.prefab", out bool success);

            if (!success)
            {
                Debug.LogError("An error happened while trying to create the book's pages. No clue what the error is.");
                return null;
            }

            return prefab;
        }
    }
}
