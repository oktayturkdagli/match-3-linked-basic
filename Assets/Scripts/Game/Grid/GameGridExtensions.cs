using UnityEngine;

namespace Match3Linked.Game
{
    public static class GameGridExtensions
    {
        public static GameGridElement GetElement(this GameGrid grid, int column, int row)
        {
            return grid.Elements[row * grid.ColumnCount + column];
        }
        
        public static Vector2 GridToWorldPosition(this GameGrid grid, int column, int row)
        {
            return grid.StartCellPosition + grid.CellSize * new Vector2(column, row);
        }
        
        public static Vector2 GridToWorldPosition(this GameGrid grid, Vector2Int gridPos)
        {
            return grid.GridToWorldPosition(gridPos.x, gridPos.y);
        }
        
        public static int GridPositionToIndex(this GameGrid grid, Vector2Int gridPos)
        {
            return grid.ColumnCount * gridPos.y + gridPos.x;
        }
    }
}