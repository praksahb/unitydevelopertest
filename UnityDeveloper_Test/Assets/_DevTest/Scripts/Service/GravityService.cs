using UnityEngine;
using DevTest.Player.Service;

namespace DevTest.Gravity
{
    public class GravityService : GenericSingleton<GravityService>
    {
        [SerializeField] private Transform _environment;
        [SerializeField] private Vector3 _hologramOffset = new Vector3(0, 1.5f, 0);

        private GravityController _gravityController;
        private GravityModel _gravityModel;

        protected override void Awake()
        {
            base.Awake();
            _gravityModel = new GravityModel();
            _gravityController = new GravityController(_gravityModel, _environment, _hologramOffset);
        }

        private void Update()
        {
            if (PlayerService.Instance == null || PlayerService.Instance.GetPlayerTransform() == null) return;
            var playerView = PlayerService.Instance.GetPlayerTransform().GetComponent<DevTest.Player.PlayerView>();

            _gravityController.HandleInput(playerView);
        }
    }
}
