using UnityEngine;

namespace BetaKors.Core
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected void HandleSingleton()
        {
            if (Instance == null)
            {
                Instance = this as T;
                return;
            }

            Debug.LogWarning($"There can only be one {typeof(T).Name} instance at a time!");
        }
    }
}
