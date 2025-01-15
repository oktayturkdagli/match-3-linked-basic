using System;
using UnityEngine;

namespace Match3Linked.Game
{
    /// <summary>
    /// Represents information about a specific element in the game grid.
    /// This class holds data regarding the color and sprite associated with a grid element.
    /// </summary>
    [Serializable]
    public class GridElementInfo
    {
        [SerializeField] public ColorType colorType;
        [SerializeField] public Sprite sprite;
    }
}