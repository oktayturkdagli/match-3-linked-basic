using TMPro;
using UnityEngine;

namespace Match3Linked.Game
{
    public class MoveCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counterText;

        private void Start()
        {
            UpdateText();
        }
        
        private void OnEnable()
        {
            GameEvents.OnElementsDespawned.AddListener(OnElementsDespawned);
        }
        
        private void OnDisable()
        {
            GameEvents.OnElementsDespawned.RemoveListener(OnElementsDespawned);
        }
        
        private void OnElementsDespawned(int count)
        {
            UpdateText();
        }
        
        private void UpdateText()
        {
            counterText.text = GameManager.Instance.MovesAvailable.ToString();
        }
    }
}