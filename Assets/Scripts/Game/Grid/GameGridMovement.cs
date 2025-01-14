using System.Collections;
using UnityEngine;

namespace Match3Linked.Game
{
    public class GameGridMovement
    {
        private readonly GameGrid _grid;

        public GameGridMovement(GameGrid grid)
        {
            _grid = grid;
        }
        
        public IEnumerator WaitForMovement()
        {
            MoveElements();

            while (!IsMovementDone())
            {
                yield return new WaitForSeconds(0.05f);
            }
        }
        
        private bool IsMovementDone()
        {
            foreach (GameGridElement element in _grid.Elements)
            {
                if (element.IsSpawned && element.IsMoving)
                {
                    return false;
                }
            }

            return true;
        }
        
        private void MoveElements()
        {
            // Run from bottom to top through all rows
            for (int y = 0; y < _grid.RowCount; y++)
            {
                for (int x = 0; x < _grid.ColumnCount; x++)
                {
                    ProcessCell(x, y);
                }
            }
        }
        
        private void ProcessCell(int column, int row)
        {
            GameGridElement element = _grid.GetElement(column, row);

            // Is the element not spawned? 
            // => Cell is empty and elements above should move downwards
            if (!element.IsSpawned)
            {
                //Move the next element above it down
                for (int i = row + 1; i < _grid.RowCount; i++)
                {
                    GameGridElement next = _grid.GetElement(column, i);

                    if (next && next.IsSpawned && !next.IsMoving)
                    {
                        MoveElement(new Vector2Int(column, i), new Vector2Int(column, row));

                        return;
                    }
                }
            }
        }

        private void MoveElement(Vector2Int oldPos, Vector2Int newPos)
        {
            //Catch the elements of the two grid positions
            GameGridElement element1 = _grid.GetElement(oldPos.x, oldPos.y);
            GameGridElement element2 = _grid.GetElement(newPos.x, newPos.y);

            //Switch them in the grid
            _grid.Elements[_grid.GridPositionToIndex(oldPos)] = element2;
            _grid.Elements[_grid.GridPositionToIndex(newPos)] = element1;

            //Update world positions
            element2.transform.position = _grid.GridToWorldPosition(oldPos);
            element1.Move(_grid.GridToWorldPosition(newPos), 0.4f);
        }
    }
}