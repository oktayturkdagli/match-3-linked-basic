using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3Linked.Core
{
    public class SceneLoadingManager : Singleton<SceneLoadingManager>
    {
        private const float FadeDuration = 0.5f;
        private CanvasGroup _canvasGroup;

        protected override void Awake()
        {
            base.Awake();
            _canvasGroup = GetComponentInChildren<CanvasGroup>();
        }
        
        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            StopAllCoroutines();
            StartCoroutine(LoadSceneCoroutine(sceneName, mode));
        }
        
        private IEnumerator LoadSceneCoroutine(string sceneName, LoadSceneMode mode)
        {
            yield return StartCoroutine(_canvasGroup.FadeInCoroutine(FadeDuration));
            SceneManager.LoadScene(sceneName, mode);
            
            yield return new WaitForSeconds(0.25f);
            yield return StartCoroutine(_canvasGroup.FadeOutCoroutine(FadeDuration * 1.5f));
        }
    }
}