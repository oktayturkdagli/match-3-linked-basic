using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked
{
    [AddComponentMenu("UI/Vertical Gradient")]
    public class VerticalGradient : BaseMeshEffect
    {
        public Color topColor = Color.white;
        public Color bottomColor = Color.white;
        
        public override void ModifyMesh(VertexHelper vh)
        {
            if (enabled)
            {
                UIVertex vertex = default;
                float[] t = { 0f, 1f, 1f, 0f };

                for (int i = 0; i < vh.currentVertCount; i++)
                {
                    vh.PopulateUIVertex(ref vertex, i);
                    vertex.color *= Color.Lerp(bottomColor, topColor, t[i]);
                    vh.SetUIVertex(vertex, i);
                }
            }
        }
    }
}