using UnityEngine;

namespace Match3Linked.Game
{
    public class SceneManager : SingletonPersistent<SceneManager>
    {
        private static Color _defaultFadeColor = Color.black;
        
        public void LoadScene(string sceneName, float duration = 1f)
        {
            Transition.LoadLevel(sceneName, duration, _defaultFadeColor);
        }
    }
}