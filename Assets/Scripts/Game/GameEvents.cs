using UnityEngine.Events;

namespace Match3Linked.Game
{
    /// <summary>
    /// Event for broadcasting an integer value.
    /// </summary>
    public class IntEvent : UnityEvent<int> { }

    /// <summary>
    /// Event for broadcasting changes in the score, with the previous and current score.
    /// </summary>
    public class ScoreChangedEvent : UnityEvent<int, int> { }

    /// <summary>
    /// A static class to hold game-related events.
    /// </summary>
    public static class GameEvents
    {
        /// <summary>
        /// Triggered when elements are despawned.
        /// </summary>
        public static IntEvent OnElementsDespawned { get; } = new();

        /// <summary>
        /// Triggered when the selection is changed.
        /// </summary>
        public static IntEvent OnSelectionChanged { get; } = new();

        /// <summary>
        /// Triggered when the score changes.
        /// </summary>
        public static ScoreChangedEvent OnScoreChanged { get; } = new();
    }
}