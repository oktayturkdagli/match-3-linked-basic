using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Match3Linked
{
    /// <summary>
    /// This class is responsible for managing the transitions between scenes
    /// that are performed in the game via a classic fade to/from black.
    /// </summary>
    public class Transition : MonoBehaviour
    {
        private static GameObject _canvasObject;

        private void Awake()
        {
            _canvasObject = new GameObject("TransitionCanvas");
            var canvas = _canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            DontDestroyOnLoad(_canvasObject);
        }

        public static void LoadLevel(string level, float duration, Color fadeColor)
        {
            var fade = new GameObject("Transition");
            fade.AddComponent<Transition>();
            fade.GetComponent<Transition>().StartFade(level, duration, fadeColor);
            fade.transform.SetParent(_canvasObject.transform, false);
            fade.transform.SetAsLastSibling();
        }

        private void StartFade(string level, float duration, Color fadeColor)
        {
            StartCoroutine(RunFade(level, duration, fadeColor));
        }

        private IEnumerator RunFade(string level, float duration, Color fadeColor)
        {
            var bgTex = new Texture2D(1, 1);
            bgTex.SetPixel(0, 0, fadeColor);
            bgTex.Apply();

            var overlay = new GameObject();
            var image = overlay.AddComponent<Image>();
            var rect = new Rect(0, 0, bgTex.width, bgTex.height);
            var sprite = Sprite.Create(bgTex, rect, new Vector2(0.5f, 0.5f), 1);
            image.material.mainTexture = bgTex;
            image.sprite = sprite;
            var newColor = image.color;
            image.color = newColor;
            image.canvasRenderer.SetAlpha(0.0f);

            overlay.transform.localScale = new Vector3(1, 1, 1);
            overlay.GetComponent<RectTransform>().sizeDelta = _canvasObject.GetComponent<RectTransform>().sizeDelta;
            overlay.transform.SetParent(_canvasObject.transform, false);
            overlay.transform.SetAsFirstSibling();

            var time = 0.0f;
            var halfDuration = duration / 2.0f;
            while (time < halfDuration)
            {
                time += Time.deltaTime;
                image.canvasRenderer.SetAlpha(Mathf.InverseLerp(0, 1, time / halfDuration));
                yield return new WaitForEndOfFrame();
            }

            image.canvasRenderer.SetAlpha(1.0f);
            yield return new WaitForEndOfFrame();

            SceneManager.LoadScene(level);

            time = 0.0f;
            while (time < halfDuration)
            {
                time += Time.deltaTime;
                image.canvasRenderer.SetAlpha(Mathf.InverseLerp(1, 0, time / halfDuration));
                yield return new WaitForEndOfFrame();
            }

            image.canvasRenderer.SetAlpha(0.0f);
            yield return new WaitForEndOfFrame();

            Destroy(_canvasObject);
        }
    }
}
