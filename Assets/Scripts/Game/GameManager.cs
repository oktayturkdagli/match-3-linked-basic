using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Linked.Game
{
    /// <summary>
    /// Manages the overall game flow, including game state, player moves, and score.
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private Button backButton;
        [SerializeField] private GameGrid gameGrid;
        [SerializeField] private int movesAvailable = 20;

        public static int Score { get; private set; }

        /// <summary>
        /// The number of remaining moves the player can make.
        /// </summary>
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

            // Start the main game loop
            yield return StartCoroutine(RunGameLoop());

            // End the game after the loop
            yield return StartCoroutine(EndGame());
        }

        /// <summary>
        /// Handles the back button press event to navigate to the menu.
        /// </summary>
        private void OnBackButtonClick()
        {
            SceneLoadingManager.Instance.LoadScene(SceneNames.Menu);
        }

        private void Update()
        {
            // Check for the Escape key press to trigger back button press
            if (Input.GetKey(KeyCode.Escape))
            {
                OnBackButtonClick();
            }
        }

        /// <summary>
        /// Initializes the game by resetting the score and setting up the grid.
        /// </summary>
        private void InitializeGame()
        {
            Score = 0;
            gameGrid.SetUpGrid();
        }

        /// <summary>
        /// Main game loop that handles the gameplay.
        /// </summary>
        /// <returns>Returns an enumerator for the game loop.</returns>
        private IEnumerator RunGameLoop()
        {
            while (MovesAvailable > 0)
            {
                // Wait for the player to select elements on the grid
                yield return gameGrid.WaitForSelection();

                // Despawn selected elements from the grid
                yield return gameGrid.DespawnSelection();

                // Wait for grid elements to complete their movement animations
                yield return gameGrid.WaitForMovement();

                // Respawn elements that were despawned
                yield return gameGrid.RespawnElements();
            }
        }

        /// <summary>
        /// Ends the game by transitioning to the game over scene.
        /// </summary>
        /// <returns>Returns an enumerator for the game end sequence.</returns>
        private IEnumerator EndGame()
        {
            yield return new WaitForSeconds(0.5f);
            SceneLoadingManager.Instance.LoadScene(SceneNames.GameOver);
        }

        /// <summary>
        /// Handles the event when elements are despawned and updates the score.
        /// </summary>
        /// <param name="despawnedCount">The number of elements that were despawned.</param>
        private void OnElementsDespawned(int despawnedCount)
        {
            int previousScore = Score;
            
            // The scoring formula is based on the number of despawned elements
            Score += despawnedCount * (despawnedCount - 1);

            // Trigger the score change event
            GameEvents.OnScoreChanged.Invoke(previousScore, Score);
        }
    }
}
