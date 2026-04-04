using UnityEngine;

namespace CricketSimulation
{
    /// <summary>
    /// Handles the positioning of the bounce marker on the pitch with constraints.
    /// </summary>
    public class BounceMarker : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float moveSpeed = 10f;
        
        [Header("Constraints")]
        [Tooltip("Assign a BoxCollider representing the valid bounce area (top half of the pitch).")]
        [SerializeField] private BoxCollider allowedBounds;

        private Vector3 startPosition;

        private void Awake()
        {
            startPosition = transform.position;
        }

        private void Start()
        {
            if (allowedBounds == null)
            {
                Debug.LogWarning("BounceMarker: No allowedBounds collider assigned! Marker will be free-roaming.");
            }
        }

        public void HandleInput()
        {
            float h = Input.GetAxisRaw("Horizontal"); // A/D
            float v = Input.GetAxisRaw("Vertical");   // W/S

            if (h == 0 && v == 0) return;

            Vector3 movement = new Vector3(h, 0, v) * moveSpeed * Time.deltaTime;
            Vector3 nextPosition = transform.position + movement;

            // Clamp to bounds if assigned
            if (allowedBounds != null)
            {
                Bounds bounds = allowedBounds.bounds;
                nextPosition.x = Mathf.Clamp(nextPosition.x, bounds.min.x, bounds.max.x);
                nextPosition.z = Mathf.Clamp(nextPosition.z, bounds.min.z, bounds.max.z);
                // Keep y constant for the marker
                nextPosition.y = transform.position.y;
            }

            transform.position = nextPosition;
        }

        public void ResetMarker()
        {
            transform.position = startPosition;
        }

        public Vector3 GetTargetPosition()
        {
            return transform.position;
        }
    }
}
