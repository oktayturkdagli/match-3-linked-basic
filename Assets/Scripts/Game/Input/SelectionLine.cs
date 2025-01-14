using System.Collections.Generic;
using UnityEngine;

namespace Match3Linked.Game
{
    [RequireComponent(typeof(LineRenderer))]
    public class SelectionLine : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Color _color;
        
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                _lineRenderer.startColor = value;
                _lineRenderer.endColor = value;
            }
        }

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }
        
        public void SetPositions(List<GameGridElement> selectedElements)
        {
            _lineRenderer.positionCount = selectedElements.Count;
            
            for (int i = 0; i < selectedElements.Count; i++)
            {
                _lineRenderer.SetPosition(i, selectedElements[i].transform.position);
            }
        }
        
        public void Clear()
        {
            _lineRenderer.positionCount = 0;
        }
    }
}