using UnityEngine;

namespace RealMan
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static object m_Lock = new object();
        private static T m_Instance = null;
        internal static T Instance
        {
            get
            {
                lock (m_Lock)
                {
                    if (Instance == null)
                    {
                        m_Instance = (T)Object.FindObjectOfType(typeof(T));
                        if (m_Instance == null)
                        {
                            m_Instance = new GameObject(nameof(T)).AddComponent<T>();
                            Object.DontDestroyOnLoad(m_Instance.gameObject);
                        }
                    }

                    return Instance;
                }
            }
        }

    }
}