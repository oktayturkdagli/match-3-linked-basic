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
        
        private void Start()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            DisplayHighScore();
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(OnPlayButtonClicked);
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        }
        
        /// <summary>
        /// Loads the game scene when the play button is clicked.
        /// </summary>
        private void OnPlayButtonClicked()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Game);
        }
        
        /// <summary>
        /// Loads the settings scene when the settings button is clicked.
        /// </summary>
        private void OnSettingsButtonClicked()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Settings);
        }

        /// <summary>
        /// Displays the current high score on the UI.
        /// </summary>
        private void DisplayHighScore()
        {
            highScoreText.text = HighScoreManager.Instance.HighScore.ToString();
        }
    }
}