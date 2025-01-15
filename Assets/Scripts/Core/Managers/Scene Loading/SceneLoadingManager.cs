using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3Linked.Core
{
    /// <summary>
    /// Manages scene loading with fade transitions.
    /// </summary>
    public class SceneLoadingManager : Singleton<SceneLoadingManager>
    {
        private const float FadeDuration = 0.5f;
        private CanvasGroup _fadeCanvasGroup;

        protected override void Awake()
        {
            base.Awake();
            // Ensure the CanvasGroup is properly retrieved from the children
            _fadeCanvasGroup = GetComponentInChildren<CanvasGroup>();
        }
        
        /// <summary>
        /// Starts loading the specified scene with a fade transition.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        /// <param name="loadMode">The load mode for the scene (default is Single).</param>
        public void LoadScene(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            StopAllCoroutines(); // Stop any ongoing loading process
            StartCoroutine(LoadSceneCoroutine(sceneName, loadMode)); // Start loading the new scene with fade effect
        }
        
        /// <summary>
        /// Coroutine to load the scene with fade transitions.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        /// <param name="loadMode">The load mode for the scene.</param>
        /// <returns>Enumerator for coroutine.</returns>
        private IEnumerator LoadSceneCoroutine(string sceneName, LoadSceneMode loadMode)
        {
            // Perform fade-in before scene loading
            yield return StartCoroutine(FadeInCoroutine());

            // Load the new scene
            SceneManager.LoadScene(sceneName, loadMode);

            // Wait briefly before starting the fade-out transition
            yield return new WaitForSeconds(0.25f);

            // Perform fade-out after scene loading
            yield return StartCoroutine(FadeOutCoroutine());
        }

        /// <summary>
        /// Fades in the UI over a set duration.
        /// </summary>
        /// <returns>Enumerator for coroutine.</returns>
        private IEnumerator FadeInCoroutine()
        {
            yield return _fadeCanvasGroup.FadeInCoroutine(FadeDuration);
        }

        /// <summary>
        /// Fades out the UI over a set duration.
        /// </summary>
        /// <returns>Enumerator for coroutine.</returns>
        private IEnumerator FadeOutCoroutine()
        {
            yield return _fadeCanvasGroup.FadeOutCoroutine(FadeDuration * 1.5f);
        }
    }
}