using System.Collections;
using UnityEngine;

namespace BetaKors.Extensions
{
    public static class IEnumeratorExtensions
    {
        public static Coroutine StartCoroutine(this IEnumerator coroutine, MonoBehaviour owner)
        {
            return owner.StartCoroutine(coroutine);
        }
    }
}
