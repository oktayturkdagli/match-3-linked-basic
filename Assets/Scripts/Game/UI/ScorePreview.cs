using TMPro;
using System.Collections;
using UnityEngine;

namespace Match3Linked.Game
{
    /// <summary>
    /// Handles the display of the score preview when selecting elements in the game.
    /// </summary>
    public class ScorePreview : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scorePreviewText;
        
        private float _currentScore;
        
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
            ResetScorePreview();
        }

        /// <summary>
        /// Resets the score preview when elements are despawned.
        /// </summary>
        /// <param name="count">The number of elements that were despawned.</param>
        private void OnElementsDespawned(int count)
        {
            ResetScorePreview();
        }

        /// <summary>
        /// Resets the score preview to an empty state.
        /// </summary>
        private void ResetScorePreview()
        {
            scorePreviewText.text = string.Empty;
            _currentScore = 0;
        }

        /// <summary>
        /// Updates the score preview based on the number of selected elements.
        /// </summary>
        /// <param name="selectionCount">The number of selected elements.</param>
        private void OnSelectionChanged(int selectionCount)
        {
            if (selectionCount > 1)
            {
                int scorePreview = selectionCount * (selectionCount - 1);

                StopAllCoroutines();
                StartCoroutine(UpdateScorePreviewCoroutine((int)_currentScore, scorePreview, 0.5f)); // Start a coroutine to animate the score.
            }
            else
            {
                StopAllCoroutines();
                scorePreviewText.text = string.Empty;
            }
        }

        /// <summary>
        /// Coroutine to smoothly update the score preview text over time.
        /// </summary>
        /// <param name="startValue">The starting score value.</param>
        /// <param name="endValue">The target score value.</param>
        /// <param name="duration">The duration of the transition.</param>
        /// <returns>Yield instruction for the coroutine.</returns>
        private IEnumerator UpdateScorePreviewCoroutine(int startValue, int endValue, float duration)
        {
            float elapsedTime = 0.0f;
            float initialTime = Time.timeSinceLevelLoad;

            // While the duration is not complete, animate the score value.
            while (elapsedTime < duration)
            {
                elapsedTime += Time.timeSinceLevelLoad - initialTime;
                initialTime = Time.timeSinceLevelLoad;

                // Update the current score with a smooth transition.
                _currentScore = Mathf.Lerp(startValue, endValue, elapsedTime / duration);

                // Update the UI text to reflect the current score.
                scorePreviewText.text = $"+ {(int)_currentScore}";

                yield return null;
            }

            // Finalize the score text to the target value.
            scorePreviewText.text = $"+ {endValue}";
        }
    }
}