using TMPro;
using System.Collections;
using UnityEngine;

namespace Match3Linked.Game
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        
        private void OnEnable()
        {
            GameEvents.OnScoreChanged.AddListener(OnScoreChanged);
        }
        
        private void OnDisable()
        {
            GameEvents.OnScoreChanged.RemoveListener(OnScoreChanged);
        }
        
        private void OnScoreChanged(int oldScore, int newScore)
        {
            StopAllCoroutines();
            StartCoroutine(UpdateTextCoroutine(oldScore, newScore, 1.0f));
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

                // Update text component with the interpolated value for the score
                float value = Mathf.Lerp(from, to, elapsedTime / time);
                text.text = ((int)value).ToString();

                yield return null;
            }

            text.text = to.ToString();
        }
    }
}