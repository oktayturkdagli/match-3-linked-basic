using TMPro;
using Match3Linked.Core;
using Match3Linked.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked.Game
{
    /// <summary>
    /// Handles the user interface for the Game Over screen, including score display, restart functionality, and navigation to the main menu.
    /// </summary>
    public class GameOverUI : UserInterface
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button backButton;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject newHighScoreMessage;

        private void Start()
        {
            restartButton.onClick.AddListener(OnRestartButtonClick);
            backButton.onClick.AddListener(OnBackButtonClick);

            DisplayScore();
            CheckForNewHighScore();
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(OnRestartButtonClick);
            backButton.onClick.RemoveListener(OnBackButtonClick);
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

        /// <summary>
        /// Handles the restart button click event, reloading the game scene.
        /// </summary>
        private void OnRestartButtonClick()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Game);
        }

        /// <summary>
        /// Handles the back button click event, navigating back to the main menu.
        /// </summary>
        protected override void OnBackButtonClick()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Menu);
        }
    }
}