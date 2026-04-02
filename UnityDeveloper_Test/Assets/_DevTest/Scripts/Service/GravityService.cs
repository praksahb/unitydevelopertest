using UnityEngine;
using DevTest.Gravity;
using DevTest.Service;

namespace DevTest.Service
{
    public class GravityService : GenericSingleton<GravityService>
    {
        [SerializeField] private Transform _environment;

        private GravityController _gravityController;
        private GravityModel _gravityModel;

        protected override void Awake()
        {
            base.Awake();
            _gravityModel = new GravityModel();
            _gravityController = new GravityController(_gravityModel, _environment);
        }

        private void Update()
        {
            if (PlayerService.Instance == null || PlayerService.Instance.GetPlayerTransform() == null) return;
            var playerView = PlayerService.Instance.GetPlayerTransform().GetComponent<DevTest.Player.PlayerView>();

            _gravityController.HandleInput(playerView);
        }
    }
}
