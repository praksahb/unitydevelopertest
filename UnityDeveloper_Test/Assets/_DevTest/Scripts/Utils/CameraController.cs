using DevTest.Player.Service;
using UnityEngine;

namespace DevTest
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset = new Vector3(0, 3, -5); 
        [SerializeField] private Vector3 _lookAtOffset = new Vector3(0, 1.5f, 0);
        [SerializeField] private float _smoothSpeed = 10f;

        private Transform _target;

        private void LateUpdate()
        {
            if (_target == null && PlayerService.Instance != null)
            {
                _target = PlayerService.Instance.GetPlayerTransform();
            }
            if (_target == null) return;


            // Without Rotation

            // 1. Calculate the target position with pure World offset
            Vector3 targetPosition = _target.position + _offset;
            
            // 2. Smoothly move the camera
            transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);

            // 3. Point the camera at the player
            transform.LookAt(_target.position + _lookAtOffset);


            // With Rotation 

            //// 1. Calculate the target position relative to the player's rotation
            //Vector3 desiredPosition = _target.position + (_target.rotation * _offset);

            //// 2. Smoothly move the camera to that position
            //transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);

            //// 3. Point the camera at a spot above the player's pivot (e.g., their head)
            //Vector3 lookTarget = _target.position + (_target.rotation * _lookAtOffset);
            //transform.LookAt(lookTarget, _target.up);
        }
    }
}
