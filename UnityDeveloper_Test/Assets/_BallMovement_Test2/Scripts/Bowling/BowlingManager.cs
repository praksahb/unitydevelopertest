using UnityEngine;

namespace CricketSimulation
{
    public class BowlingManager : MonoBehaviour
    {
        public enum DeliverySide { OffSide = -1, LegSide = 1 }
        public enum State { Aiming, Metering, Simulation }

        [Header("Components")]
        [SerializeField] private BallController _ball;
        [SerializeField] private BounceMarker _marker;

        [Header("Spawn Points")]
        [SerializeField] private Transform[] _spawnPoints;
        private int _currentSpawnIndex = 0;

        [Header("Physics Tuning")]
        [SerializeField] private float _ballSpeed = 25f;
        [Range(0f, 100f)]
        [SerializeField] private float _swingForce = 50f;
        [Range(0f, 1f)]
        [SerializeField] private float _spinForce = 0.2f;

        [Header("Delivery Settings")]
        [SerializeField] private bool _isSwingMode = true; 
        [SerializeField] private DeliverySide _deliverySide = DeliverySide.LegSide;

        // not being used presently. can be used to add in-accuracy to bounce marker
        [SerializeField] private float _maxMissOffset = 1.5f;

        public bool IsSwingMode => _isSwingMode;

        public State currentState { get; private set; } = State.Aiming;

        private void Start() => ResetAll();

        private void Update()
        {
            if (currentState == State.Simulation && Input.GetKeyDown(KeyCode.R)) 
                ResetAll();
            
            if (currentState == State.Aiming)
                _marker.HandleInput();
        }

        // --- PUBLIC API FOR UIMANAGER ---

        public void StartMetering()
        {
            if (currentState != State.Aiming) return;
            currentState = State.Metering;
        }

        public void Launch(float accuracy)
        {
            if (currentState != State.Metering) return;
            
            currentState = State.Simulation;
            
            // Get physics properties
            Vector3 start = _ball.transform.position;
            Vector3 target = _marker.GetTargetPosition();

            // Calculate directional vectors
            Vector3 diff = target - start;
            Vector3 horizontalDir = new Vector3(diff.x, 0, diff.z);
            // Swapping the cross order to flip the "Right" direction
            Vector3 lateralDir = Vector3.Cross(Vector3.up, horizontalDir.normalized);
            float dist = horizontalDir.magnitude;

            float t = dist / _ballSpeed;
            float g = Mathf.Abs(Physics.gravity.y);
            float vy = (diff.y + 0.5f * g * t * t) / t;
            
            Vector3 velocity = horizontalDir.normalized * _ballSpeed;
            velocity.y = vy;

            // Prepare Ball
            float strength = (float)_deliverySide * accuracy;
            if (_isSwingMode)
                _ball.Prepare(strength, 0f, lateralDir, _swingForce, _spinForce);
            else
                _ball.Prepare(0f, strength, lateralDir, _swingForce, _spinForce);

            _ball.Launch(velocity);
        }

        public void ToggleSpawnPoint()
        {
            if (currentState != State.Aiming) return;
            _currentSpawnIndex = (_currentSpawnIndex + 1) % _spawnPoints.Length;
            ResetAll();
        }

        public void SetSwingMode(bool swing) => _isSwingMode = swing;

        public void ResetAll()
        {
            if (_spawnPoints != null && _spawnPoints.Length > 0)
            {
                Transform activeSpawn = _spawnPoints[_currentSpawnIndex];
                _ball.transform.SetParent(activeSpawn, false);
                _ball.transform.localPosition = Vector3.zero;
                _ball.transform.localRotation = Quaternion.identity;
            }

            _ball.ResetPhysics();
            _marker.ResetMarker();
            currentState = State.Aiming;
        }
    }
}
