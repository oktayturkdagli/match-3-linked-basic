using TMPro;
using System.Collections;
using UnityEngine;

namespace Match3Linked.Game
{
    public class ScorePreview : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scorePreviewText;
        private float _current;
        
        private void OnEnable()
        {
            GameEvents.OnSelectionChanged.AddListener(OnSelectionChanged);
            GameEvents.OnElementsDespawned.AddListener(OnElementsDespawned);
        }
        
        private void OnDisable()
        {
            GameEvents.OnSelectionChanged.RemoveListener(OnSelectionChanged);
            GameEvents.OnElementsDespawned.RemoveListener(OnElementsDespawned);
        }

        private void Start()
        {
            ResetPreview();
        }
        
        private void OnElementsDespawned(int count)
        {
            ResetPreview();
        }
        
        private void ResetPreview()
        {
            scorePreviewText.text = string.Empty;
            _current = 0;
        }
        
        private void OnSelectionChanged(int count)
        {
            if (count > 1)
            {
                int scoreRevenuePreview = count * (count - 1);

                StopAllCoroutines();
                StartCoroutine(UpdateTextCoroutine((int)_current, scoreRevenuePreview, 0.5f));
            }
            else
            {
                StopAllCoroutines();
                scorePreviewText.text = string.Empty;
            }
        }
        
        private IEnumerator UpdateTextCoroutine(int from, int to, float time)
        {
            float currentTime = Time.timeSinceLevelLoad;
            float elapsedTime = 0.0f;
            float lastTime = currentTime;

            while (time > 0 && elapsedTime < time)
            {
                // Update Time
                currentTime = Time.timeSinceLevelLoad;
                elapsedTime += currentTime - lastTime;
                lastTime = currentTime;

                // Update current value
                _current = Mathf.Lerp(from, to, elapsedTime / time);

                // Update the UI text component
                scorePreviewText.text = "+ " + ((int)_current);

                yield return null;
            }

            scorePreviewText.text = "+ " + to;
        }
    }
}