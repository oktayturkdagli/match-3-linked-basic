using System.Collections.Generic;
using UnityEngine;

namespace Match3Linked.Game
{
    public class GameGrid : MonoBehaviour
    {
        [SerializeField] private int rowCount = 8;
        [SerializeField] private int columnCount = 8;
        [SerializeField] private float cellSize = 1.0f;
        [SerializeField] private SelectionLine selectionLine;
        [SerializeField] private GameGridElement gridElementPrefab;
        [SerializeField] private List<GameGridElementInfo> gameGridElementInfoList = new();

        private GameGridInput _gridInput;
        private GameGridMovement _gridMovement;
        private GameGridSpawning _gridSpawning;
        
        public List<GameGridElement> Elements { get; } = new();
        public List<GameGridElementInfo> GameGridElementInfoList => gameGridElementInfoList;
        public Vector2 StartCellPosition => GridCenter - CellSize * new Vector2(ColumnCount - 1, RowCount - 1) / 2.0f;
        public float CellSize => cellSize;
        public int RowCount => rowCount;
        public int ColumnCount => columnCount;
        private Vector2 GridCenter => transform.position;
        private Transform GridContainer => transform;

        private void Awake()
        {
            _gridInput = new GameGridInput(this, selectionLine);
            _gridMovement = new GameGridMovement(this);
            _gridSpawning = new GameGridSpawning(this);
        }
        
        public void SetUpGrid()
        {
            for (int y = 0; y < ColumnCount; y++)
            {
                for (int x = 0; x < RowCount; x++)
                {
                    GameGridElement element = Instantiate(gridElementPrefab, GridContainer.transform, true);
                    element.transform.localScale = Vector2.one * cellSize;
                    element.transform.position = this.GridToWorldPosition(x, y);
                    var randomElementInfo = GameGridElementInfoList.GetRandom();
                    element.Sprite = randomElementInfo.sprite;
                    element.ColorType = randomElementInfo.colorType;
                    Elements.Add(element);
                }
            }
        }
        
        public Coroutine WaitForMovement()
        {
            return StartCoroutine(_gridMovement.WaitForMovement());
        }
        
        public Coroutine WaitForSelection()
        {
            return StartCoroutine(_gridInput.WaitForSelection());
        }
        
        public Coroutine RespawnElements()
        {
            return StartCoroutine(_gridSpawning.RespawnElements());
        }
        
        public Coroutine DespawnSelection()
        {
            return StartCoroutine(_gridSpawning.Despawn(_gridInput.SelectedElements));
        }
    }
}