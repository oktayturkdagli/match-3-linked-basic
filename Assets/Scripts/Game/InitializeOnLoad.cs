using UnityEngine;

namespace Match3Linked.Game
{
    /// <summary>
    /// This class is responsible for initializing necessary game objects before any scene is loaded.
    /// </summary>
    internal static class ApplicationInitializer
    {
        // The key used to identify prefabs that should be initialized on application load
        private const string InitializeOnLoadKey = "InitializeOnLoad/";

        /// <summary>
        /// Initializes the application by loading and instantiating prefabs from a specific Resources folder.
        /// The instantiated objects are set to not be destroyed on scene load.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeApplication()
        {
            var prefabsToInitialize = Resources.LoadAll<GameObject>(InitializeOnLoadKey);

            if (prefabsToInitialize.Length > 0)
            {
                foreach (var prefab in prefabsToInitialize)
                {
                    GameObject instantiatedObject = Object.Instantiate(prefab);
                    instantiatedObject.name = prefab.name;

                    Object.DontDestroyOnLoad(instantiatedObject);
                }
            }
            else
            {
                Debug.LogWarning("No prefabs found in the 'InitializeOnLoad' Resources folder.");
            }
        }
    }
}