using UnityEngine;

namespace Match3Linked.Game
{
    /// <summary>
    /// Provides extension methods for the ColorType enum.
    /// </summary>
    public static class ColorTypeExtensions
    {
        /// <summary>
        /// Maps a ColorType to a corresponding Unity Color.
        /// </summary>
        /// <param name="colorType">The ColorType to map.</param>
        /// <returns>The Unity Color corresponding to the specified ColorType.</returns>
        public static Color ToUnityColor(this ColorType colorType)
        {
            return colorType switch
            {
                ColorType.Color1 => Color.yellow,
                ColorType.Color2 => Color.blue,
                ColorType.Color3 => Color.green,
                ColorType.Color4 => Color.red,
                _ => Color.yellow
            };
        }
    }
}