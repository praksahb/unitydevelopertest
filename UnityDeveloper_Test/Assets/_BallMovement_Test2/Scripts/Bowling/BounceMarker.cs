using UnityEngine;

namespace CricketSimulation
{
    /// <summary>
    /// Handles the positioning of the bounce marker on the pitch with constraints.
    /// </summary>
    public class BounceMarker : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _moveSpeed = 10f;
        
        [Header("Constraints")]
        [Tooltip("Assign a BoxCollider representing the valid bounce area.")]
        [SerializeField] private BoxCollider _allowedBounds;

        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;
        }

        public void HandleInput()
        {
            // Standardizing to W/A/S/D mapping from Input Manager
            float h = Input.GetAxisRaw("Horizontal"); 
            float v = Input.GetAxisRaw("Vertical");   

            if (h == 0 && v == 0) return;

            Vector3 movement = new Vector3(h, 0, v) * _moveSpeed * Time.deltaTime;
            Vector3 nextPosition = transform.position + movement;

            // Clamp to pitch bounds if assigned
            if (_allowedBounds != null)
            {
                Bounds bounds = _allowedBounds.bounds;
                nextPosition.x = Mathf.Clamp(nextPosition.x, bounds.min.x, bounds.max.x);
                nextPosition.z = Mathf.Clamp(nextPosition.z, bounds.min.z, bounds.max.z);
                nextPosition.y = transform.position.y; // Keep vertical height locked
            }

            transform.position = nextPosition;
        }

        public void ResetMarker()
        {
            transform.position = _startPosition;
        }

        public Vector3 GetTargetPosition()
        {
            return transform.position;
        }
    }
}
