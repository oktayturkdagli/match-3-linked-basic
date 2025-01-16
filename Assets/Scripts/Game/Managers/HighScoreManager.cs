using UnityEngine;

namespace Match3Linked.Game
{
    /// <summary>
    /// Manages the high score for the game, providing functionality to get and set the high score.
    /// The high score is saved using Unity's PlayerPrefs.
    /// </summary>
    public class HighScoreManager : Singleton<HighScoreManager>
    {
        // The key used to store the high score in PlayerPrefs
        private const string HighScoreKey = "HighScore";

        /// <summary>
        /// Gets the current high score. If no high score exists, returns 0.
        /// </summary>
        public int HighScore
        {
            // Check if the high score exists in PlayerPrefs
            get => PlayerPrefs.HasKey(HighScoreKey) ? PlayerPrefs.GetInt(HighScoreKey) : 0;
            set
            {
                // Save the new high score and apply changes
                PlayerPrefs.SetInt(HighScoreKey, value);
                PlayerPrefs.Save();
            }
        }

        /// <summary>
        /// Clears the high score by removing it from PlayerPrefs.
        /// </summary>
        public void ClearHighScore()
        {
            PlayerPrefs.DeleteKey(HighScoreKey);
            PlayerPrefs.Save();
        }
    }
}