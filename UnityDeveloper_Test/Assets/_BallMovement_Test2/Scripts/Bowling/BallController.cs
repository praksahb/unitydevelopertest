using UnityEngine;

namespace CricketSimulation
{
    public class BallController : MonoBehaviour
    {
        private Rigidbody _rb;
        private bool _isInAir = false;
        private float _swingDirection = 0f; 
        private float _spinDirection = 0f;  
        private float _airTime = 0f;
        private Vector3 _sideVector = Vector3.right;

        // Physics values passed from Manager
        private float _swingForce;
        private float _spinForce;
        private float _bounceFactor = 1f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        public void Prepare(float swing, float spin, Vector3 lateralDir, float sForce, float spForce)
        {
            _swingDirection = swing;
            _spinDirection = spin;
            _sideVector = lateralDir;
            _swingForce = sForce;
            _spinForce = spForce;
        }

        public void Launch(Vector3 velocity)
        {
            _rb.isKinematic = false;
            _rb.linearVelocity = velocity;
            _isInAir = true;
            _airTime = 0f;
        }

        public void ResetPhysics()
        {
            _isInAir = false;
            _rb.isKinematic = true;
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            if (_isInAir)
            {
                _airTime += Time.fixedDeltaTime;
                // Phase I: Swing
                float forceX = _swingDirection * _swingForce * _airTime;
                _rb.AddForce(_sideVector * forceX, ForceMode.Acceleration);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!_isInAir) return;

            // early return flag allows a single collision logic to be entertained
            _isInAir = false; 

            // Phase II: Spin
            _rb.AddForce(_spinDirection * _spinForce * _sideVector, ForceMode.Impulse);

            Vector3 v = _rb.linearVelocity;
            v.y *= _bounceFactor;
            _rb.linearVelocity = v;
        }
    }
}
