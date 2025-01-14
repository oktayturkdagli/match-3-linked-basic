using TMPro;
using Match3Linked.Core;
using Match3Linked.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked.Game
{
    public class MenuUI : UserInterface
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private TextMeshProUGUI highScoreText;

        private void Start()
        {
            // Listen to button click events
            playButton.onClick.AddListener(LoadGameScene);
            settingsButton.onClick.AddListener(LoadSettingsScene);

            // Set high score text
            highScoreText.text = HighScoreManager.Instance.HighScore.ToString();
        }
        
        private void LoadGameScene()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Game);
        }
        
        private void LoadSettingsScene()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Settings);
        }
        
        protected override void OnBackButtonClick()
        {
            ExitApplication();
        }

        private void ExitApplication()
        {
            PlayerPrefs.Save();
            Application.Quit();
        }
    }
}