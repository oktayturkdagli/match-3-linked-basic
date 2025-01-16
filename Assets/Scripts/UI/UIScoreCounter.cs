using TMPro;
using System.Collections;
using Match3Linked.Game;
using UnityEngine;

namespace Match3Linked.UI
{
    /// <summary>
    /// Manages the score UI and updates the displayed score with animation.
    /// </summary>
    public class UIScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private void OnEnable()
        {
            GameEvents.OnScoreChanged.AddListener(OnScoreChanged);
        }

        private void OnDisable()
        {
            GameEvents.OnScoreChanged.RemoveListener(OnScoreChanged);
        }

        /// <summary>
        /// Handles the score change event and starts the score animation coroutine.
        /// </summary>
        /// <param name="oldScore">The previous score before the change.</param>
        /// <param name="newScore">The new score after the change.</param>
        private void OnScoreChanged(int oldScore, int newScore)
        {
            StopAllCoroutines();
            StartCoroutine(AnimateScoreChange(oldScore, newScore, 1.0f));
        }

        /// <summary>
        /// Animates the score text change from the old score to the new score over time.
        /// </summary>
        /// <param name="startScore">The starting score value.</param>
        /// <param name="endScore">The target score value.</param>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>A coroutine that updates the score text.</returns>
        private IEnumerator AnimateScoreChange(int startScore, int endScore, float duration)
        {
            // Track elapsed time
            float elapsedTime = 0.0f;
            float initialTime = Time.timeSinceLevelLoad;

            // Continuously update the score until the animation is complete
            while (elapsedTime < duration)
            {
                // Calculate the interpolation value based on elapsed time
                elapsedTime += Time.timeSinceLevelLoad - initialTime;
                initialTime = Time.timeSinceLevelLoad;

                // Interpolate the score value
                float interpolatedScore = Mathf.Lerp(startScore, endScore, elapsedTime / duration);

                // Update the score text
                scoreText.text = Mathf.RoundToInt(interpolatedScore).ToString();

                yield return null;
            }

            // Ensure the final score is displayed
            scoreText.text = endScore.ToString();
        }
    }
}