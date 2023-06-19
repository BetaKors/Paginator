using System.Linq;
using UnityEditor;
using UnityEngine;

// Adapted by BetaKors. Originally authored by Senshi.
// Download: https://forum.unity.com/attachments/uguitools-cs.110408
#pragma warning disable IDE1006
public class uGUITools : MonoBehaviour
#pragma warning restore IDE1006
{
    [MenuItem("uGUI/Anchors to Corners %[")]
    private static void AnchorsToCorners()
    {
        foreach (var transform in Selection.transforms)
        {
            var rt = transform as RectTransform;
            var prt = transform.parent as RectTransform;

            Undo.RecordObject(rt, "Anchors to Corners");

            rt.anchorMin = new(
                rt.anchorMin.x + rt.offsetMin.x / prt.rect.width,
                rt.anchorMin.y + rt.offsetMin.y / prt.rect.height
            );

            rt.anchorMax = new(
                rt.anchorMax.x + rt.offsetMax.x / prt.rect.width,
                rt.anchorMax.y + rt.offsetMax.y / prt.rect.height
            );

            rt.offsetMin = rt.offsetMax = Vector2.zero;
        }
    }

    [MenuItem("uGUI/Corners to Anchors %]")]
    private static void CornersToAnchors()
    {
        foreach (var transform in Selection.transforms)
        {
            var rt = transform as RectTransform;

            Undo.RecordObject(rt, "Corners to Anchors");

            rt.offsetMin = rt.offsetMax = Vector2.zero;
        }
    }

    [MenuItem("uGUI/Anchors to Corners %[", true)]
    private static bool AnchorsToCornersCheck() => Selection.transforms.All(t => t is RectTransform && t.parent is RectTransform);

    [MenuItem("uGUI/Corners to Anchors %]", true)]
    private static bool CornersToAnchorsCheck() => Selection.transforms.All(t => t is RectTransform);
}
