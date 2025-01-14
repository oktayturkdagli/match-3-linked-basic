using System;
using UnityEngine;

namespace Match3Linked.Game
{
    [Serializable]
    public class GameGridElementInfo
    {
        [SerializeField] public ColorType colorType;
        [SerializeField] public Sprite sprite;
    }
}