using UnityEngine;

namespace Match3Linked.Core.UI
{
    public abstract class UserInterface : MonoBehaviour
    {
        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnBackButtonClick();
            }
        }
        
        protected abstract void OnBackButtonClick();
    }
}