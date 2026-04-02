using UnityEngine;
using DevTest.Player;

namespace DevTest.Gravity
{
    public class GravityController
    {
        private GravityModel _model;
        private Transform _environment;

        public GravityController(GravityModel model, Transform environment)
        {
            _model = model;
            _environment = environment;
        }

        public void HandleInput(PlayerView playerView)
        {
            if (playerView == null || playerView.Hologram == null) return;

            bool inputReceived = false;

            // Simple Axis Selection
            if (Input.GetKeyDown(KeyCode.UpArrow)) 
            { 
                _model.TargetRotationAxis = Vector3.right; 
                _model.TargetRotationAngle = 90f; 
                inputReceived = true; 
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) 
            { 
                _model.TargetRotationAxis = Vector3.right; 
                _model.TargetRotationAngle = -90f; 
                inputReceived = true; 
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
            { 
                _model.TargetRotationAxis = Vector3.forward; 
                _model.TargetRotationAngle = 90f; 
                inputReceived = true; 
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) 
            { 
                _model.TargetRotationAxis = Vector3.forward; 
                _model.TargetRotationAngle = -90f; 
                inputReceived = true; 
            }

            if (inputReceived)
            {
                _model.IsSelecting = true;
                playerView.Hologram.SetActive(true);
            }

            if (_model.IsSelecting)
            {
                // 1. Lock to player position (Simplistic version)
                playerView.Hologram.transform.position = playerView.transform.position;

                // 2. Simple Rotation Logic
                Vector3 targetUp = Vector3.up; 
                if (_model.TargetRotationAxis == Vector3.right) 
                    targetUp = (_model.TargetRotationAngle > 0) ? Vector3.back : Vector3.forward;
                else 
                    targetUp = (_model.TargetRotationAngle > 0) ? Vector3.right : Vector3.left;

                playerView.Hologram.transform.rotation = Quaternion.FromToRotation(Vector3.up, targetUp);
            }

            // Confirm Selection (Enter Key)
            if (_model.IsSelecting && Input.GetKeyDown(KeyCode.Return))
            {
                ApplyGravityShift(playerView);
            }
        }

        private void ApplyGravityShift(PlayerView playerView)
        {
            _model.IsSelecting = false;
            playerView.Hologram.SetActive(false);

            if (_environment != null)
            {
                // The "Blue Pill" World Spin Trick
                _environment.RotateAround(playerView.transform.position, _model.TargetRotationAxis, _model.TargetRotationAngle);
            }
        }
    }
}
