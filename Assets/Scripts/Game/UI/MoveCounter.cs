using TMPro;
using UnityEngine;

namespace Match3Linked.Game
{
    /// <summary>
    /// This class is responsible for updating the move counter display based on available moves.
    /// It listens for events when elements are despawned and updates the UI accordingly.
    /// </summary>
    public class MoveCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moveCountText;

        private void Start()
        {
            UpdateMoveCountText();
        }

        private void OnEnable()
        {
            GameEvents.OnElementsDespawned.AddListener(OnElementsDespawned);
        }

        private void OnDisable()
        {
            GameEvents.OnElementsDespawned.RemoveListener(OnElementsDespawned);
        }

        /// <summary>
        /// This method is called when elements are despawned, updating the move count display.
        /// </summary>
        /// <param name="despawnedElementCount">The number of elements that were despawned.</param>
        private void OnElementsDespawned(int despawnedElementCount)
        {
            UpdateMoveCountText();
        }

        /// <summary>
        /// Updates the move counter text based on the current available moves.
        /// </summary>
        private void UpdateMoveCountText()
        {
            int availableMoves = GameManager.Instance.MovesAvailable;
            moveCountText.text = availableMoves.ToString();
        }
    }
}