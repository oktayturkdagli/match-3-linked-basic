using UnityEngine;

namespace Match3Linked
{
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        _instance = SetupInstance();
                    }
                    else
                    {
                        Debug.Log($"[PersistentSingleton] Instance of {typeof(T).Name} already exists: {_instance.gameObject.name}");
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (!_instance)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private static T SetupInstance()
        {
            var singletonObject = new GameObject(typeof(T).Name);
            var instance = singletonObject.AddComponent<T>();
            DontDestroyOnLoad(singletonObject);
            return instance;
        }
    }
}