using UnityEngine;

namespace Match3Linked
{
    /// <summary>
    /// Helper component to transition from one scene to another.
    /// </summary>
    public class ScreenTransition : MonoBehaviour
    {
        public string scene = "<Insert scene name>";
        public float duration = 1.0f;
        public Color color = Color.black;

        public void PerformTransition()
        {
            Transition.LoadLevel(scene, duration, color);
        }
    }
}
