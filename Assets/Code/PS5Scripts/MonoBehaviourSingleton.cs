using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is used for singletons that must prevail througout
//the program lifetime
public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
{
    [Header("Singleton Setting")]
    [SerializeField] bool destroyGameObjectIfDuplicate = true;

    private static T m_instance;
    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject go = new GameObject(typeof(T).Name + " Instance");
                DontDestroyOnLoad(go);
                go.hideFlags = HideFlags.HideInInspector;
                m_instance = go.AddComponent<T>();
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this.GetComponent<T>();
            DontDestroyOnLoad(m_instance);
        }
        else
        {
            if (m_instance != this.GetComponent<T>())
            {
                if (!destroyGameObjectIfDuplicate) Destroy(this.GetComponent<T>());
                else Destroy(this.gameObject);
            }
        }
    }
}
