using System.Collections;
using System.Collections.Generic;
using Match3Linked.Core;
using UnityEngine;

namespace Match3Linked.Game
{
    public class GameGridInput
    {
        private readonly GameGrid _grid;
        private readonly SelectionLine _selectionLine;
        private Camera _camera;

        public List<GameGridElement> SelectedElements { get; }

        public GameGridInput(GameGrid grid, SelectionLine selectionLine)
        {
            _grid = grid;
            _selectionLine = selectionLine;
            SelectedElements = new List<GameGridElement>();
        }
        
        private void Clear()
        {
            _selectionLine.Clear();
            SelectedElements.Clear();
            GameEvents.OnSelectionChanged.Invoke(SelectedElements.Count);
        }
        
        public IEnumerator WaitForSelection()
        {
            Clear();

            while (true)
            {
                yield return null;
                
                // Does the user touch the screen?
                if (Input.GetMouseButton(0))
                {
                    //Does the user touch an element?
                    if (InputRaycast(out GameGridElement element))
                    {
                        ProcessGridElement(element);
                    }
                }
                
                // Selection finished?
                else if (SelectedElements.Count > 0)
                {
                    if (IsValidInput())
                    {
                        // Clear selection line when finished
                        _selectionLine.Clear();

                        // Stop coroutine
                        yield break;
                    }

                    Clear();
                }
            }
        }
        
        private bool InputRaycast(out GameGridElement element)
        {
            element = null;
            _camera ??= Camera.main;
            
            // Get mouse position
            if (_camera)
            {
                Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

                // Shoot a raycast on z-level
                RaycastHit2D hitInfo = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hitInfo)
                {
                    // Try to get the game grid element
                    element = hitInfo.transform.GetComponent<GameGridElement>();

                    // Return true if the hit object is GameGridElement
                    if (element)
                        return true;
                }
            }

            // Return false if nothing is hit or if the object is not GameGridElement
            return false;
        }
        
        private void ProcessGridElement(GameGridElement element)
        {
            // No element selected yet? Select element!
            if (SelectedElements.Count == 0)
            {
                // Cache selected color
                _selectionLine.Color = element.ColorType.GetColor();

                //Add element to selection
                AddElementToSelection(element);
            }
            else
            {
                // Is the element already selected?
                if (SelectedElements.Contains(element))
                {
                    // Did the player moved back? Deselect element!
                    if (IsSecondLast(element))
                    {
                        DeselectLast();
                    }
                }
                
                // Not selected, correct color and in distance? Select element!
                else if (IsSelectable(element))
                {
                    AddElementToSelection(element);
                }
            }
        }
        
        private bool IsSecondLast(GameGridElement element)
        {
            return SelectedElements.Count >= 2 && element.Equals(SelectedElements[^2]);
        }
        
        private bool IsSelectable(GameGridElement element)
        {
            return HasValidColor(element) && IsInDistance(element);
        }
        
        private bool HasValidColor(GameGridElement element)
        {
            return _selectionLine.Color == element.ColorType.GetColor();
        }
        
        private bool IsInDistance(GameGridElement element)
        {
            Vector2 lastElementPos = SelectedElements[^1].transform.position;
            return Vector2.Distance(element.transform.position, lastElementPos) < 1.5f * _grid.CellSize;
        }
        
        private void AddElementToSelection(GameGridElement element)
        {
            if (SelectedElements.Count > 0)
            {
                float pitch = 1.0f + SelectedElements.Count * 0.1f;
                AudioManager.Instance.PlayOneShot("selection", pitch);
            }

            SelectedElements.Add(element);
            _selectionLine.Color = element.ColorType.GetColor();
            _selectionLine.SetPositions(SelectedElements);
            GameEvents.OnSelectionChanged.Invoke(SelectedElements.Count);
        }
        
        private void DeselectLast()
        {
            SelectedElements.Remove(SelectedElements[^1]);
            _selectionLine.SetPositions(SelectedElements);
            GameEvents.OnSelectionChanged.Invoke(SelectedElements.Count);
        }
        
        private bool IsValidInput()
        {
            return SelectedElements.Count > 2;
        }
    }
}