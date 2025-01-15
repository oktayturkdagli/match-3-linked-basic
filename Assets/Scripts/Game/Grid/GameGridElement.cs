using UnityEngine;

namespace Match3Linked.Game
{
    /// <summary>
    /// Represents a single element in the game grid.
    /// Handles element movement, spawning, despawning, and visual updates.
    /// </summary>
    public class GameGridElement : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer elementSpriteRenderer;

        /// <summary>
        /// Indicates whether the element is currently moving.
        /// </summary>
        public bool IsMoving { get; private set; }

        /// <summary>
        /// Indicates whether the element is active in the scene.
        /// </summary>
        public bool IsSpawned => gameObject.activeSelf;

        /// <summary>
        /// The color type of the grid element (e.g., for match 3 logic).
        /// </summary>
        public ColorType ColorType { get; set; }

        /// <summary>
        /// The sprite of the element.
        /// </summary>
        public Sprite Sprite
        {
            get => elementSpriteRenderer.sprite;
            set => elementSpriteRenderer.sprite = value;
        }

        /// <summary>
        /// Moves the grid element to a target position over a specified duration.
        /// </summary>
        /// <param name="targetPosition">The target position to move the element to.</param>
        /// <param name="duration">The time over which the movement will occur.</param>
        public void MoveTo(Vector3 targetPosition, float duration)
        {
            IsMoving = true;
            StartCoroutine(transform.MoveCoroutine(targetPosition, duration, () =>
            {
                IsMoving = false;
            }));
        }

        /// <summary>
        /// Spawns the grid element in the scene, scaling it from a smaller size to the target scale.
        /// </summary>
        /// <param name="targetScaleFactor">The final scale of the element.</param>
        public void Spawn(float targetScaleFactor)
        {
            gameObject.SetActive(true);
            Vector2 initialScale = Vector2.one * 0.1f;
            Vector2 finalScale = Vector2.one * targetScaleFactor;
            StartCoroutine(transform.ScaleCoroutine(initialScale, finalScale, 0.25f));
        }

        /// <summary>
        /// Despawns the grid element from the scene, scaling it down before disabling it.
        /// </summary>
        /// <param name="initialScaleFactor">The scale of the element before despawning.</param>
        public void Despawn(float initialScaleFactor)
        {
            Vector2 initialScale = Vector2.one * initialScaleFactor;
            Vector2 finalScale = Vector2.one * 0.1f;
            StartCoroutine(transform.ScaleCoroutine(initialScale, finalScale, 0.25f, () =>
            {
                gameObject.SetActive(false);
            }));
        }
    }
}
