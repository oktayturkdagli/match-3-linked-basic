using Match3Linked.Core;
using Match3Linked.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked.Game
{
    public class SettingsUI : UserInterface
    {
        [SerializeField] private Button backButton;

        private void Start()
        {
            backButton.onClick.AddListener(OnBackButtonClick);
        }
        
        protected override void OnBackButtonClick()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Menu);
        }
    }
}