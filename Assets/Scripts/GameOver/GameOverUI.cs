using TMPro;
using Match3Linked.Core;
using Match3Linked.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked.Game
{
    public class GameOverUI : UserInterface
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button backButton;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject highScoreMessage;

        private void Start()
        {
            restartButton.onClick.AddListener(OnRestartButtonClick);
            backButton.onClick.AddListener(OnBackButtonClick);
            
            scoreText.text = GameManager.Score.ToString();
            if (GameManager.Score > HighScoreManager.Instance.HighScore)
            {
                HighScoreManager.Instance.HighScore = GameManager.Score;
                highScoreMessage.SetActive(true);
            }
        }
        
        private void OnRestartButtonClick()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Game);
        }
        
        protected override void OnBackButtonClick()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Menu);
        }
    }
}