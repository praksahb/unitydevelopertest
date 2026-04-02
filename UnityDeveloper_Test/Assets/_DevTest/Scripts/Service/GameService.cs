using UnityEngine;

namespace DevTest.Service
{
    public class GameService : GenericSingleton<GameService>
    {
        [Header("Settings")]
        [SerializeField] private float _deathMagnitude = 50f;

        private GameModel _model;
        private GameManager _gameManager;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            // 1. Initialize SCMV Components
            _model = new GameModel();
            
            // Link cubes via CollectionService (Safe, as Start happens after all Awakes)
            if (CollectionService.Instance != null)
                _model.TotalCubes = CollectionService.Instance.TotalCubes;

            _gameManager = new GameManager(_model, PlayerService.Instance, _deathMagnitude);

            // 2. Link UI
            _gameManager.OnGameStateUpdate += (time, score) => 
            {
                if (UIService.Instance != null)
                {
                    UIService.Instance.UpdateUI(time, score);
                }
            };

            _gameManager.OnGameOver += HandleGameOver;
        }

        private void Update()
        {
            _gameManager?.HandleUpdate();
        }

        private void HandleGameOver(string message)
        {
            if (UIService.Instance != null)
            {
                UIService.Instance.ShowGameEnd(message);
            }
        }
    }
}
