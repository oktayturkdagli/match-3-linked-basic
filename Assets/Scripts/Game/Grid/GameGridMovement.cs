using System.Collections;
using UnityEngine;

namespace Match3Linked.Game
{
    /// <summary>
    /// Handles the movement of elements on the game grid during gameplay.
    /// </summary>
    public class GameGridMovement
    {
        private readonly GameGrid _grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameGridMovement"/> class.
        /// </summary>
        /// <param name="grid">The game grid that this movement system will operate on.</param>
        public GameGridMovement(GameGrid grid)
        {
            _grid = grid;
        }
        
        /// <summary>
        /// Coroutine to wait until all elements in the grid have completed their movement.
        /// </summary>
        /// <returns>An enumerator to be used in a coroutine.</returns>
        public IEnumerator WaitForMovementToComplete()
        {
            // Start the movement of elements
            MoveElements();

            // Wait until all elements have finished moving
            while (!IsMovementComplete())
            {
                yield return new WaitForSeconds(0.05f);
            }
        }
        
        /// <summary>
        /// Checks if all elements in the grid have completed their movement.
        /// </summary>
        /// <returns>True if all elements have stopped moving; otherwise, false.</returns>
        private bool IsMovementComplete()
        {
            foreach (GameGridElement element in _grid.Elements)
            {
                if (element.IsSpawned && element.IsMoving)
                {
                    return false; // If any element is still moving, return false
                }
            }

            return true; // All elements have finished moving
        }
        
        /// <summary>
        /// Initiates the movement process of elements in the grid.
        /// Moves elements from the bottom to the top of the grid.
        /// </summary>
        private void MoveElements()
        {
            // Loop through rows from top to bottom
            for (int row = 0; row < _grid.RowCount; row++)
            {
                for (int column = 0; column < _grid.ColumnCount; column++)
                {
                    ProcessCell(column, row);
                }
            }
        }
        
        /// <summary>
        /// Processes a specific cell to move elements down if necessary.
        /// </summary>
        /// <param name="column">The column index of the cell.</param>
        /// <param name="row">The row index of the cell.</param>
        private void ProcessCell(int column, int row)
        {
            GameGridElement currentElement = _grid.GetElementAt(column, row);

            // If no element is spawned in the current cell, it means this cell is empty, and
            // we need to move elements from above it downwards
            if (!currentElement.IsSpawned)
            {
                MoveElementDownIfNeeded(column, row);
            }
        }

        /// <summary>
        /// Moves an element down from a higher row into an empty cell.
        /// </summary>
        /// <param name="column">The column index of the empty cell.</param>
        /// <param name="emptyRow">The row index of the empty cell.</param>
        private void MoveElementDownIfNeeded(int column, int emptyRow)
        {
            // Look for the first spawned element above the empty cell
            for (int rowAbove = emptyRow + 1; rowAbove < _grid.RowCount; rowAbove++)
            {
                GameGridElement elementAbove = _grid.GetElementAt(column, rowAbove);

                if (elementAbove && elementAbove.IsSpawned && !elementAbove.IsMoving)
                {
                    // Move the element down to the empty cell
                    MoveElement(new Vector2Int(column, rowAbove), new Vector2Int(column, emptyRow));
                    return; // Only move one element at a time
                }
            }
        }

        /// <summary>
        /// Moves an element from one position to another on the grid.
        /// </summary>
        /// <param name="oldPosition">The current position of the element.</param>
        /// <param name="newPosition">The target position for the element.</param>
        private void MoveElement(Vector2Int oldPosition, Vector2Int newPosition)
        {
            // Retrieve the elements at the two positions
            GameGridElement oldElement = _grid.GetElementAt(oldPosition.x, oldPosition.y);
            GameGridElement newElement = _grid.GetElementAt(newPosition.x, newPosition.y);

            // Swap their positions in the grid's element array
            _grid.Elements[_grid.ConvertGridPositionToIndex(oldPosition)] = newElement;
            _grid.Elements[_grid.ConvertGridPositionToIndex(newPosition)] = oldElement;

            // Update the world position of the elements
            newElement.transform.position = _grid.ConvertGridToWorldPosition(oldPosition);
            oldElement.MoveTo(_grid.ConvertGridToWorldPosition(newPosition), 0.4f);
        }
    }
}
