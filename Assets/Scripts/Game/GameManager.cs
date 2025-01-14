using Match3Linked.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked.Game
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private Button backButton;
        [SerializeField] private GameGrid grid;
        [SerializeField] private int movesAvailable = 20;
        
        public static int Score { get; private set; }
        
        public int MovesAvailable
        {
            get => movesAvailable;
            set => movesAvailable = value;
        }
        
        private void OnEnable()
        {
            GameEvents.OnElementsDespawned.AddListener(OnElementsDespawned);
        }

        private void OnDisable()
        {
            GameEvents.OnElementsDespawned.RemoveListener(OnElementsDespawned);
        }
        
        private IEnumerator Start()
        {
            backButton.onClick.AddListener(OnBackButtonClick);
            InitializeGame();

            // Wait for the game to be executed completely
            yield return StartCoroutine(RunGame());

            // Wait for the game to finish
            yield return StartCoroutine(EndGame());
        }
        
        private void OnBackButtonClick()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Menu);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                OnBackButtonClick();
            }
        }
        
        private void InitializeGame()
        {
            Score = 0;

            grid.SetUpGrid();
        }
        
        private IEnumerator RunGame()
        {
            // Game Loop
            while (MovesAvailable > 0)
            {
                // Wait for the Player to select elements
                yield return grid.WaitForSelection();

                // Despawn selected elements
                yield return grid.DespawnSelection();

                // Wait for the grid elements to finish movement
                yield return grid.WaitForMovement();

                // Respawn despawned elements
                yield return grid.RespawnElements();
            }
        }
        
        private IEnumerator EndGame()
        {
            yield return new WaitForSeconds(0.5f);

            SceneLoadingManager.Instance.LoadScene(SceneNames.GameOver);
        }
        
        private void OnElementsDespawned(int count)
        {
            // Update score
            int oldScore = Score;
            Score = oldScore + count * (count - 1);

            // Invoke score changed event
            GameEvents.OnScoreChanged.Invoke(oldScore, Score);
        }
    }
}