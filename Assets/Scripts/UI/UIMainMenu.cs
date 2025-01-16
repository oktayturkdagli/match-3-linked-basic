using TMPro;
using Match3Linked.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked.UI
{
    /// <summary>
    /// Handles the UI functionality for the main menu, including loading game and settings scenes,
    /// displaying the high score, and exiting the application.
    /// </summary>
    public class UIMainMenu : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private TextMeshProUGUI highScoreText;
        

        /// <summary>
        /// Displays the current high score on the UI.
        /// </summary>
        private void DisplayHighScore()
        {
            highScoreText.text = HighScoreManager.Instance.HighScore.ToString();
        }
    }
}