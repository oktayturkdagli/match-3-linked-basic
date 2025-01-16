using System.Collections.Generic;
using UnityEngine;

namespace Match3Linked.Game
{
    /// <summary>
    /// Manages the grid of game elements, handles input, movement, and spawning of grid elements.
    /// </summary>
    public class GameGrid : MonoBehaviour
    {
        [SerializeField] private int rowCount = 8;
        [SerializeField] private int columnCount = 8;
        [SerializeField] private float cellSize = 1.0f; 
        [SerializeField] private SelectionLine selectionLine;
        [SerializeField] private GameGridElement gridElementPrefab;
        [SerializeField] private GameObject gridElementBackgroundParent;
        [SerializeField] private GameObject gridElementBackgroundPrefab;

        [SerializeField] private List<GridElementInfo> elementInfoList = new(); // List of possible grid element info

        private GameGridInput _gridInput; // Handles input actions on the grid
        private GameGridMovement _gridMovement; // Handles movement of elements in the grid
        private GameGridSpawning _gridSpawning; // Handles spawning and despawning of elements
        
        /// <summary>
        /// The collection of elements in the grid.
        /// </summary>
        public List<GameGridElement> Elements { get; } = new();

        /// <summary>
        /// The list of all available element information.
        /// </summary>
        public List<GridElementInfo> ElementInfoList => elementInfoList;

        /// <summary>
        /// The position of the starting cell (top-left corner) of the grid.
        /// </summary>
        public Vector2 StartCellPosition => GridCenter - (CellSize * new Vector2(ColumnCount - 1, RowCount - 1) / 2.0f);

        public float CellSize => cellSize;
        public int RowCount => rowCount;
        public int ColumnCount => columnCount;

        /// <summary>
        /// The center position of the grid in world space.
        /// </summary>
        private Vector2 GridCenter => transform.position;

        /// <summary>
        /// The container transform holding all grid elements.
        /// </summary>
        private Transform GridContainer => transform;

        private void Awake()
        {
            _gridInput = new GameGridInput(this, selectionLine);
            _gridMovement = new GameGridMovement(this);
            _gridSpawning = new GameGridSpawning(this);
        }
        
        /// <summary>
        /// Sets up the grid by spawning elements in each cell and assigning random element data.
        /// </summary>
        public void SetUpGrid()
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    // Instantiate a new grid element at the specified position
                    GameGridElement element = Instantiate(gridElementPrefab, GridContainer.transform, true);
                    element.transform.localScale = Vector2.one * cellSize;
                    element.transform.position = GridToWorldPosition(column, row);

                    // Assign random element data (sprite and color type)
                    var randomElementInfo = ElementInfoList.GetRandomElement();
                    element.Sprite = randomElementInfo.sprite;
                    element.ColorType = randomElementInfo.colorType;

                    // Add the newly created element to the grid elements list
                    Elements.Add(element);

                    // Instantiate a new grid element background at the specified position
                    GameObject background = Instantiate(gridElementBackgroundPrefab, gridElementBackgroundParent.transform, true);
                    background.transform.localScale = Vector2.one;
                    background.transform.position = GridToWorldPosition(column, row);
                }
            }
        }
        
        /// <summary>
        /// Waits for any grid element movement to complete.
        /// </summary>
        /// <returns>A coroutine that can be started for movement waiting.</returns>
        public Coroutine WaitForMovement()
        {
            return StartCoroutine(_gridMovement.WaitForMovementToComplete());
        }
        
        /// <summary>
        /// Waits for selection input to occur.
        /// </summary>
        /// <returns>A coroutine that can be started for selection waiting.</returns>
        public Coroutine WaitForSelection()
        {
            return StartCoroutine(_gridInput.WaitForSelection());
        }
        
        /// <summary>
        /// Respawns elements that need to be respawned (e.g., after a match or movement).
        /// </summary>
        /// <returns>A coroutine that can be started for respawning elements.</returns>
        public Coroutine RespawnElements()
        {
            return StartCoroutine(_gridSpawning.RespawnElements());
        }
        
        /// <summary>
        /// Despawns the currently selected elements.
        /// </summary>
        /// <returns>A coroutine that can be started for despawning the selected elements.</returns>
        public Coroutine DespawnSelection()
        {
            return StartCoroutine(_gridSpawning.Despawn(_gridInput.SelectedElements));
        }

        /// <summary>
        /// Converts grid coordinates (column, row) to world space position.
        /// </summary>
        /// <param name="column">The column index in the grid.</param>
        /// <param name="row">The row index in the grid.</param>
        /// <returns>The world space position corresponding to the grid coordinates.</returns>
        private Vector2 GridToWorldPosition(int column, int row)
        {
            return StartCellPosition + new Vector2(column, row) * CellSize;
        }
    }
}
