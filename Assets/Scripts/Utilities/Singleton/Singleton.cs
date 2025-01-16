using UnityEngine;

namespace Match3Linked
{
    /// <summary>
    /// A generic Singleton class for managing a single instance of a MonoBehaviour-derived type.
    /// Ensures that only one instance of the specified type exists in the scene.
    /// </summary>
    /// <typeparam name="T">The type of the singleton class, which must inherit from Component.</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        /// <summary>
        /// Gets the singleton instance of the specified type.
        /// If no instance exists, one will be created.
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
                        _instance = CreateNewInstance();
                    }
                    else
                    {
                        Debug.Log($"[Singleton] An instance of {typeof(T).Name} already exists: {_instance.gameObject.name}");
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Ensures the singleton instance is properly assigned or destroys duplicates.
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this)
            {
                Debug.LogWarning($"[Singleton] Duplicate instance of {typeof(T).Name} found. Destroying the duplicate on {gameObject.name}.");
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Creates a new GameObject with the specified singleton type as a component.
        /// </summary>
        /// <returns>The created singleton instance.</returns>
        private static T CreateNewInstance()
        {
            var newGameObject = new GameObject($"{typeof(T).Name}(Singleton)");
            var newInstance = newGameObject.AddComponent<T>();
            return newInstance;
        }
    }
}