using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked
{
    /// <summary>
    /// Adds a vertical gradient effect to a UI element's vertices.
    /// </summary>
    [AddComponentMenu("UI/Vertical Gradient")]
    public class GradientImage : BaseMeshEffect
    {
        /// <summary>
        /// The color at the top of the gradient.
        /// </summary>
        [Tooltip("The color at the top of the gradient.")]
        public Color topColor = Color.white;

        /// <summary>
        /// The color at the bottom of the gradient.
        /// </summary>
        [Tooltip("The color at the bottom of the gradient.")]
        public Color bottomColor = Color.white;

        /// <summary>
        /// Modifies the mesh to apply a vertical gradient.
        /// </summary>
        /// <param name="vh">The vertex helper for managing the mesh data.</param>
        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive() || vh.currentVertCount == 0)
                return;

            UIVertex vertex = default;

            for (int vertexIndex = 0; vertexIndex < vh.currentVertCount; vertexIndex++)
            {
                vh.PopulateUIVertex(ref vertex, vertexIndex);

                // Determine the gradient factor based on vertex position
                float gradientFactor = Mathf.InverseLerp(0, 1, vertex.position.y);

                // Apply the interpolated color
                vertex.color *= Color.Lerp(bottomColor, topColor, gradientFactor);
                vh.SetUIVertex(vertex, vertexIndex);
            }
        }
    }
}