using TMPro;
using Match3Linked.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked.UI
{
    /// <summary>
    /// Handles the user interface for the Game Over screen, including score display, restart functionality, and navigation to the main menu.
    /// </summary>
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button backButton;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject newHighScoreMessage;

        private void Start()
        {
            DisplayScore();
            CheckForNewHighScore();
        }

        /// <summary>
        /// Displays the current score on the UI.
        /// </summary>
        private void DisplayScore()
        {
            scoreText.text = GameManager.Score.ToString();
        }

        /// <summary>
        /// Checks if the current score is a new high score and displays a message if true.
        /// </summary>
        private void CheckForNewHighScore()
        {
            if (GameManager.Score > HighScoreManager.Instance.HighScore)
            {
                HighScoreManager.Instance.HighScore = GameManager.Score;
                newHighScoreMessage.SetActive(true);
            }
        }
    }
}