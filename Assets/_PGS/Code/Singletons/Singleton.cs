using PGS.Utilities;
using UnityEngine;

namespace PGS
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        [SerializeField] private bool dontDestroyOnLoad = true;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    if (instance == null)
                    {
                        Debug.Log($"No Singleton of Type {typeof(T).Name} found in scene(s). Creating a new one.");
                        instance = CommonUtilities.AddComponentToNewGameObject<T>(null, $"{typeof(T).Name} (Singleton)");
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;

                if (dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(this.gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
