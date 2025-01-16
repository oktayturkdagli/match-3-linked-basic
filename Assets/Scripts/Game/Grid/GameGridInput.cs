using System.Collections;
using System.Collections.Generic;
using Match3Linked.Game;
using UnityEngine;

namespace Match3Linked.Game
{
    /// <summary>
    /// Handles user input for selecting and interacting with elements in the game grid.
    /// </summary>
    public class GameGridInput
    {
        private readonly GameGrid _gameGrid;
        private readonly SelectionLine _selectionLine;
        private Camera _mainCamera;

        public List<GameGridElement> SelectedElements { get; private set; }

        /// <summary>
        /// Initializes a new instance of the GameGridInput class.
        /// </summary>
        /// <param name="gameGrid">The game grid object.</param>
        /// <param name="selectionLine">The selection line object that shows the selection.</param>
        public GameGridInput(GameGrid gameGrid, SelectionLine selectionLine)
        {
            _gameGrid = gameGrid;
            _selectionLine = selectionLine;
            SelectedElements = new List<GameGridElement>();
        }

        /// <summary>
        /// Clears the current selection and resets the selection line.
        /// </summary>
        private void ClearSelection()
        {
            _selectionLine.Clear();
            SelectedElements.Clear();
            GameEvents.OnSelectionChanged.Invoke(SelectedElements.Count);
        }

        /// <summary>
        /// Waits for the player to make a selection and processes it.
        /// </summary>
        /// <returns>An enumerator for the coroutine.</returns>
        public IEnumerator WaitForSelection()
        {
            ClearSelection();

            while (true)
            {
                yield return null;

                if (Input.GetMouseButton(0)) // User is touching the screen
                {
                    if (TryGetElementUnderCursor(out GameGridElement element)) // User is touching a grid element
                    {
                        OnElementSelection(element);
                    }
                }
                else if (SelectedElements.Count > 0) // Selection finished?
                {
                    if (IsSelectionValid())
                    {
                        _selectionLine.Clear(); // Clear selection line when finished
                        yield break; // Stop coroutine
                    }

                    ClearSelection(); // Reset selection if invalid
                }
            }
        }

        /// <summary>
        /// Tries to get the grid element under the mouse cursor.
        /// </summary>
        /// <param name="element">The element under the cursor.</param>
        /// <returns>True if an element was found, false otherwise.</returns>
        private bool TryGetElementUnderCursor(out GameGridElement element)
        {
            element = null;
            _mainCamera ??= Camera.main;

            if (_mainCamera)
            {
                Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hitInfo = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hitInfo)
                {
                    element = hitInfo.transform.GetComponent<GameGridElement>();
                    return element != null;
                }
            }

            return false;
        }

        /// <summary>
        /// Handles the selection or deselection of a grid element.
        /// </summary>
        /// <param name="element">The grid element to be processed.</param>
        private void OnElementSelection(GameGridElement element)
        {
            if (SelectedElements.Count == 0) // No element selected yet
            {
                _selectionLine.Color = element.ColorType.ToUnityColor();
                AddElementToSelection(element);
            }
            else
            {
                if (SelectedElements.Contains(element)) // Element already selected
                {
                    if (IsSecondLastElement(element)) // Player moved back
                    {
                        DeselectLastElement();
                    }
                }
                else if (CanSelectElement(element)) // Correct color and within distance
                {
                    AddElementToSelection(element);
                }
            }
        }

        /// <summary>
        /// Checks if the given element is the second-to-last selected element.
        /// </summary>
        /// <param name="element">The grid element to check.</param>
        /// <returns>True if the element is the second-to-last, otherwise false.</returns>
        private bool IsSecondLastElement(GameGridElement element)
        {
            return SelectedElements.Count >= 2 && element.Equals(SelectedElements[^2]);
        }

        /// <summary>
        /// Determines if an element can be selected based on color and distance.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element can be selected, false otherwise.</returns>
        private bool CanSelectElement(GameGridElement element)
        {
            return HasValidColor(element) && IsWithinSelectionDistance(element);
        }

        /// <summary>
        /// Checks if the element has the same color as the selection line.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element has a valid color, otherwise false.</returns>
        private bool HasValidColor(GameGridElement element)
        {
            return _selectionLine.Color == element.ColorType.ToUnityColor();
        }

        /// <summary>
        /// Checks if the element is within the selection distance from the last selected element.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is within the distance, false otherwise.</returns>
        private bool IsWithinSelectionDistance(GameGridElement element)
        {
            Vector2 lastElementPos = SelectedElements[^1].transform.position;
            return Vector2.Distance(element.transform.position, lastElementPos) < 1.5f * _gameGrid.CellSize;
        }

        /// <summary>
        /// Adds the element to the selection and updates the selection line.
        /// </summary>
        /// <param name="element">The element to add to the selection.</param>
        private void AddElementToSelection(GameGridElement element)
        {
            if (SelectedElements.Count > 0)
            {
                // Add a pitch variation to the sound based on selection count
                float pitch = 1.0f + SelectedElements.Count * 0.1f;
                AudioManager.Instance.PlayOneShot("selection", pitch);
            }

            SelectedElements.Add(element);
            _selectionLine.Color = element.ColorType.ToUnityColor();
            _selectionLine.SetPositions(SelectedElements);
            GameEvents.OnSelectionChanged.Invoke(SelectedElements.Count);
        }

        /// <summary>
        /// Deselects the last selected element.
        /// </summary>
        private void DeselectLastElement()
        {
            SelectedElements.RemoveAt(SelectedElements.Count - 1);
            _selectionLine.SetPositions(SelectedElements);
            GameEvents.OnSelectionChanged.Invoke(SelectedElements.Count);
        }

        /// <summary>
        /// Checks if the selection is valid (at least 3 elements).
        /// </summary>
        /// <returns>True if the selection is valid, false otherwise.</returns>
        private bool IsSelectionValid()
        {
            return SelectedElements.Count > 2;
        }
    }
}
