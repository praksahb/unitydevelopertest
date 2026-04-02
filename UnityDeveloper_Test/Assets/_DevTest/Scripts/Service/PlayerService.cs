using UnityEngine;

namespace DevTest.Player.Service
{
    public class PlayerService : GenericSingleton<PlayerService>
    {
        [SerializeField] private PlayerSO _playerStats;
        [SerializeField] private Transform _spawnPoint;

        private PlayerController _playerController;



        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            InitializePlayer();
        }

        private void InitializePlayer()
        {
            PlayerModel model = new PlayerModel(_playerStats);
            
            _playerController = new PlayerController(model, _playerStats.PlayerPrefab, _spawnPoint);
        }


        private void Update()
        {
            _playerController?.HandleUpdate();
        }

        private void FixedUpdate()
        {
            _playerController?.HandleFixedUpdate();
        }

        public Transform GetPlayerTransform() => _playerController.PlayerView.transform;
    }
}
