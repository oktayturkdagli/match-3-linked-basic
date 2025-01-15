using System;
using UnityEngine;

namespace Match3Linked.Game
{
    /// <summary>
    /// Provides extension methods for working with the <see cref="GameGrid"/> class.
    /// </summary>
    public static class GameGridExtensions
    {
        /// <summary>
        /// Gets the <see cref="GameGridElement"/> at the specified column and row position.
        /// </summary>
        /// <param name="grid">The <see cref="GameGrid"/> instance.</param>
        /// <param name="column">The column index.</param>
        /// <param name="row">The row index.</param>
        /// <returns>The <see cref="GameGridElement"/> at the specified position.</returns>
        public static GameGridElement GetElementAt(this GameGrid grid, int column, int row)
        {
            if (column < 0 || column >= grid.ColumnCount || row < 0 || row >= grid.RowCount)
                throw new ArgumentOutOfRangeException($"The provided column {column} or row {row} is out of bounds.");

            return grid.Elements[row * grid.ColumnCount + column];
        }
        
        /// <summary>
        /// Converts grid coordinates (column, row) to world position.
        /// </summary>
        /// <param name="grid">The <see cref="GameGrid"/> instance.</param>
        /// <param name="column">The column index.</param>
        /// <param name="row">The row index.</param>
        /// <returns>The world position corresponding to the grid coordinates.</returns>
        private static Vector2 ConvertGridToWorldPosition(this GameGrid grid, int column, int row)
        {
            return grid.StartCellPosition + grid.CellSize * new Vector2(column, row);
        }
        
        /// <summary>
        /// Converts grid position represented by <see cref="Vector2Int"/> to world position.
        /// </summary>
        /// <param name="grid">The <see cref="GameGrid"/> instance.</param>
        /// <param name="gridPosition">The grid position as <see cref="Vector2Int"/>.</param>
        /// <returns>The world position corresponding to the grid position.</returns>
        public static Vector2 ConvertGridToWorldPosition(this GameGrid grid, Vector2Int gridPosition)
        {
            return grid.ConvertGridToWorldPosition(gridPosition.x, gridPosition.y);
        }
        
        /// <summary>
        /// Converts a grid position to a linear index within the grid's element array.
        /// </summary>
        /// <param name="grid">The <see cref="GameGrid"/> instance.</param>
        /// <param name="gridPosition">The grid position as <see cref="Vector2Int"/>.</param>
        /// <returns>The linear index of the element in the grid.</returns>
        public static int ConvertGridPositionToIndex(this GameGrid grid, Vector2Int gridPosition)
        {
            if (gridPosition.x < 0 || gridPosition.x >= grid.ColumnCount || gridPosition.y < 0 || gridPosition.y >= grid.RowCount)
                throw new ArgumentOutOfRangeException($"The provided grid position {gridPosition} is out of bounds.");

            return grid.ColumnCount * gridPosition.y + gridPosition.x;
        }
    }
}
