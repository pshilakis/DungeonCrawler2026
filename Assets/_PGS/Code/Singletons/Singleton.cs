using PGS.Utilities;
using UnityEngine;

namespace PGS
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_Instance;

        [ColoredHeader("Generic Singleton Settings", 14, true)]
        [SerializeField] private bool dontDestroyOnLoad = true;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = FindAnyObjectByType<T>();
                    if (m_Instance == null)
                    {
                        Debug.Log($"No Singleton of Type {typeof(T).Name} found in scene(s). Creating a new one.");
                        m_Instance = CommonUtilities.AddComponentToNewGameObject<T>(null, $"{typeof(T).Name} (Singleton)");
                    }
                }

                return m_Instance;
            }
        }

        protected virtual void Awake()
        {
            if (m_Instance == null)
            {
                m_Instance = this as T;

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
