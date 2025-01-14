using UnityEngine;

namespace Match3Linked
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<T>();

                    if (!_instance)
                    {
                        _instance = SetupInstance();
                    }
                    else
                    {
                        Debug.Log($"[Singleton] Instance of {typeof(T).Name} already exists: {_instance.gameObject.name}");
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
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private static T SetupInstance()
        {
            var gameObj = new GameObject(typeof(T).Name);
            var instance = gameObj.AddComponent<T>();
            return instance;
        }
    }
}