using UnityEngine;

namespace Match3Linked
{
    /// <summary>
    /// A generic singleton class that ensures there is only one persistent instance of a component.
    /// This instance persists across scenes and is created automatically if it does not exist.
    /// </summary>
    /// <typeparam name="T">The type of the component to be a singleton.</typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        /// <summary>
        /// Gets the singleton instance of the specified type. If no instance exists, it creates one.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<T>();

                    if (!_instance)
                    {
                        _instance = CreateNewSingletonInstance();
                    }
                    else
                    {
                        Debug.Log($"[PersistentSingleton] Existing instance of {typeof(T).Name} found: {_instance.gameObject.name}");
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Ensures the singleton instance is assigned correctly and persists across scenes.
        /// Destroys duplicate instances if they exist.
        /// </summary>
        protected virtual void Awake()
        {
            if (!_instance)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Debug.LogWarning($"[PersistentSingleton] Duplicate instance of {typeof(T).Name} detected and destroyed: {gameObject.name}");
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Creates a new singleton instance by instantiating a GameObject and attaching the component.
        /// </summary>
        /// <returns>The created instance of type T.</returns>
        private static T CreateNewSingletonInstance()
        {
            var singletonObject = new GameObject($"{typeof(T).Name}_Singleton");
            var instance = singletonObject.AddComponent<T>();
            DontDestroyOnLoad(singletonObject);
            Debug.Log($"[PersistentSingleton] Created new singleton instance of {typeof(T).Name}: {singletonObject.name}");
            return instance;
        }
    }
}
