using System.Collections.Generic;
using UnityEngine;

namespace Match3Linked.Game
{
    /// <summary>
    /// Responsible for drawing a selection line between selected elements on the game grid.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class SelectionLine : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Color _color;

        /// <summary>
        /// Gets or sets the color of the selection line.
        /// </summary>
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                UpdateLineColor(value);
            }
        }

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            if (!_lineRenderer)
            {
                Debug.LogError("LineRenderer component is missing from the GameObject.");
            }
        }

        /// <summary>
        /// Sets the positions of the line renderer based on the selected grid elements.
        /// </summary>
        /// <param name="selectedElements">A list of selected grid elements.</param>
        public void SetPositions(List<GameGridElement> selectedElements)
        {
            if (selectedElements == null || selectedElements.Count == 0)
            {
                Debug.LogWarning("Selected elements list is empty or null.");
                return;
            }

            _lineRenderer.positionCount = selectedElements.Count;

            for (int i = 0; i < selectedElements.Count; i++)
            {
                _lineRenderer.SetPosition(i, selectedElements[i].transform.position);
            }
        }

        /// <summary>
        /// Clears the line renderer by resetting its position count to zero.
        /// </summary>
        public void Clear()
        {
            _lineRenderer.positionCount = 0;
        }

        /// <summary>
        /// Updates the color of the line renderer.
        /// </summary>
        /// <param name="newColor">The new color to set for the line.</param>
        private void UpdateLineColor(Color newColor)
        {
            _lineRenderer.startColor = newColor;
            _lineRenderer.endColor = newColor;
        }
    }
}
