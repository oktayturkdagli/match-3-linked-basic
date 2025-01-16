using Match3Linked.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked.UI
{
    /// <summary>
    /// Manages the settings UI in the game.
    /// Handles user interaction with the settings menu, such as navigating back to the main menu.
    /// </summary>
    public class UISettings : MonoBehaviour
    {
        [SerializeField] private Button backToMenuButton;
        
        private void Start()
        { 
            backToMenuButton.onClick.AddListener(OnBackButtonClick);
        }

        private void OnDestroy()
        {
            backToMenuButton.onClick.RemoveListener(OnBackButtonClick);
        }

        /// <summary>
        /// Handles the back button click event.
        /// Loads the main menu scene.
        /// </summary>
        private void OnBackButtonClick()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Menu);
        }
    }
}