using UnityEngine;

namespace Match3Linked.Core.UI
{
    /// <summary>
    /// Base class for handling user interface elements in the Match3Linked game.
    /// This class listens for back button actions and triggers the appropriate response.
    /// </summary>
    public abstract class UserInterface : MonoBehaviour
    {
        /// <summary>
        /// Called every frame to check for user input.
        /// If the Escape key is pressed, the OnBackButtonClick method is triggered.
        /// </summary>
        protected virtual void Update()
        {
            // Check for the Escape key press
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnBackButtonClick();
            }
        }

        /// <summary>
        /// Handles the event when the back button (Escape key) is pressed.
        /// Must be implemented by derived classes to define specific behavior.
        /// </summary>
        protected abstract void OnBackButtonClick();
    }
}