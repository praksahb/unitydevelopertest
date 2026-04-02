using UnityEngine;

namespace DevTest.Player.Service
{
    public class PlayerService : GenericSingleton<PlayerService>
    {
        [SerializeField] private PlayerSO playerStats;
        [SerializeField] private Transform spawnPoint;

        private PlayerController playerController;

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
            PlayerModel model = new PlayerModel(playerStats);
            
            playerController = new PlayerController(model, playerStats.PlayerPrefab, spawnPoint);
        }


        private void Update()
        {
            playerController?.HandleUpdate();
        }

        private void FixedUpdate()
        {
            playerController?.HandleFixedUpdate();
        }
    }
}
