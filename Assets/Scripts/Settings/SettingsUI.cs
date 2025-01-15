using Match3Linked.Core;
using Match3Linked.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked.Game
{
    /// <summary>
    /// Manages the settings UI in the game.
    /// Handles user interaction with the settings menu, such as navigating back to the main menu.
    /// </summary>
    public class SettingsUI : UserInterface
    {
        [SerializeField] private Button backToMenuButton;
        
        private void Start()
        { 
            backToMenuButton.onClick.AddListener(OnBackToMenuButtonClick);
        }

        private void OnDestroy()
        {
            backToMenuButton.onClick.RemoveListener(OnBackToMenuButtonClick);
        }

        /// <summary>
        /// Handles the back button click event.
        /// Loads the main menu scene.
        /// </summary>
        protected override void OnBackButtonClick()
        {
            LoadMainMenu();
        }

        /// <summary>
        /// Loads the main menu scene.
        /// </summary>
        private void LoadMainMenu()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Menu);
        }

        /// <summary>
        /// Handles the event when the back button is clicked.
        /// This is used to return to the main menu.
        /// </summary>
        private void OnBackToMenuButtonClick()
        {
            LoadMainMenu();
        }
    }
}