using TMPro;
using Match3Linked.Core;
using Match3Linked.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked.Game
{
    /// <summary>
    /// Handles the UI functionality for the main menu, including loading game and settings scenes,
    /// displaying the high score, and exiting the application.
    /// </summary>
    public class MainMenuUI : UserInterface
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
        /// Displays the current high score on the UI.
        /// </summary>
        private void DisplayHighScore()
        {
            highScoreText.text = HighScoreManager.Instance.HighScore.ToString();
        }

        /// <summary>
        /// Loads the game scene when the play button is clicked.
        /// </summary>
        private void OnPlayButtonClicked()
        {
            LoadScene(SceneNames.Game);
        }

        /// <summary>
        /// Loads the settings scene when the settings button is clicked.
        /// </summary>
        private void OnSettingsButtonClicked()
        {
            LoadScene(SceneNames.Settings);
        }

        /// <summary>
        /// Loads the specified scene by name.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        private void LoadScene(string sceneName)
        {
            SceneLoadingManager.Instance.LoadScene(sceneName);
        }

        /// <summary>
        /// Handles back button click (typically used to exit the application).
        /// </summary>
        protected override void OnBackButtonClick()
        {
            ExitApplication();
        }

        /// <summary>
        /// Exits the application and saves the player preferences.
        /// </summary>
        private void ExitApplication()
        {
            // Save player preferences before quitting
            PlayerPrefs.Save();

            // Exit the application
            Application.Quit();
        }
    }
}