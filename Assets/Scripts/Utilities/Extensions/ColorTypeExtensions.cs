using Match3Linked.Game;
using UnityEngine;

namespace Match3Linked
{
    public static class ColorTypeExtensions
    {
        public static Color GetColor(this ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.Color1:
                    return Color.yellow;
                case ColorType.Color2:
                    return Color.blue;
                case ColorType.Color3:
                    return Color.green;
                case ColorType.Color4:
                    return Color.red;
                default:
                    return Color.yellow;
            }
        }
    }
}