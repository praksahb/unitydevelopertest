using UnityEngine;
using UnityEngine.UI;

namespace CricketSimulation
{
    /// <summary>
    /// Handles the vertical accuracy/power meter UI logic.
    /// 100% (Maximum) is at the center, 0% at the top/bottom edges.
    /// </summary>
    public class BowlingMeterUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private RectTransform _meterContainer;

        [Header("Meter Settings")]
        [SerializeField] private float _moveSpeed = 500f;
        
        private float _currentPos = 0.5f; // 0 (bottom) to 1 (top)
        private bool _isMoving = false;
        private int _moveDirection = 1;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void StartMeter()
        {
            gameObject.SetActive(true);
            _isMoving = true;
            _currentPos = 0.5f; // Start at center (Blue zone)
        }

        public void StopMeter() => _isMoving = false;

        public void Hide() => gameObject.SetActive(false);

        public float GetAccuracy()
        {
            // Divide 0-1 range into 7 discrete zones matching your UI images
            int zone = Mathf.FloorToInt(_currentPos * 7);
            zone = Mathf.Clamp(zone, 0, 6);

            // Returns discrete accuracy based on the color zone the indicator is in:
            // [0:Red, 1:Yellow, 2:Green, 3:Blue, 4:Green, 5:Yellow, 6:Red]
            switch (zone)
            {
                case 3: return 1.0f; // Perfect Blue Center
                case 2:
                case 4: return 0.7f; // Good Green Zone
                case 1:
                case 5: return 0.4f; // Decent Yellow Zone
                case 0:
                case 6: return 0.0f; // Poor Red Edge (Flat delivery)
                default: return 0.0f;
            }
        }

        private void Update()
        {
            if (!_isMoving) return;

            // Update position (normalized 0-1)
            _currentPos += _moveDirection * (_moveSpeed / _meterContainer.rect.height) * Time.deltaTime;

            // Bounce at edges
            if (_currentPos >= 1.0f) { _currentPos = 1.0f; _moveDirection = -1; }
            else if (_currentPos <= 0.0f) { _currentPos = 0.0f; _moveDirection = 1; }

            // Move the black line indicator
            if (_indicator != null)
            {
                // Map 0-1 range to the container's height (center is 0 in anchoredPosition if pivot is centered)
                float targetY = (_currentPos - 0.5f) * _meterContainer.rect.height;
                _indicator.anchoredPosition = new Vector2(0, targetY);
            }
        }
    }
}
