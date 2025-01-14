using UnityEngine.Events;

namespace Match3Linked.Game
{
    public class UnityIntEvent : UnityEvent<int> { }
    public class ScoreChangedEvent : UnityEvent<int, int> { }
    
    public static class GameEvents
    {
        public static UnityIntEvent OnElementsDespawned { get; } = new();
        public static UnityIntEvent OnSelectionChanged { get; } = new();
        public static ScoreChangedEvent OnScoreChanged { get; } = new();
    }
}