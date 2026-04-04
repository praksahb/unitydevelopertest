using UnityEngine;
using UnityEngine.UI;

namespace CricketSimulation
{
    /// <summary>
    /// Handles all UI interactions and delegates logic to the BowlingManager.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BowlingManager _bowlingManager;
        [SerializeField] private BowlingMeterUI _meterUI;

        [Header("UI Buttons")]
        [SerializeField] private Button _swingButton;
        [SerializeField] private Button _spinButton;
        [SerializeField] private Button _bowlButton;
        [SerializeField] private Button _spawnButton;
        [SerializeField] private Button _resetButton;

        [Header("Selection Colors")]
        [SerializeField] private Color _activeColor = Color.green;
        [SerializeField] private Color _inactiveColor = Color.white;

        private void Start()
        {
            // Initializing listeners via code for a cleaner architecture
            _swingButton.onClick.AddListener(OnSwingButtonClicked);
            _spinButton.onClick.AddListener(OnSpinButtonClicked);
            _bowlButton.onClick.AddListener(OnBowlButtonClicked);
            _spawnButton.onClick.AddListener(OnSpawnToggleClicked);
            
            if (_resetButton != null)
                _resetButton.onClick.AddListener(OnResetButtonClicked);

            UpdateModeUI();
        }

        private void UpdateModeUI()
        {
            if (_swingButton == null || _spinButton == null) return;
            
            bool isSwing = _bowlingManager.IsSwingMode;
            _swingButton.image.color = isSwing ? _activeColor : _inactiveColor;
            _spinButton.image.color = !isSwing ? _activeColor : _inactiveColor;
        }

        // --- BUTTON CALLBACKS ---

        private void OnSwingButtonClicked()
        {
            if (_bowlingManager.currentState != BowlingManager.State.Aiming) return;
            _bowlingManager.SetSwingMode(true);
            UpdateModeUI();
            Debug.Log("UIManager: Mode set to Swing");
        }

        private void OnSpinButtonClicked()
        {
            if (_bowlingManager.currentState != BowlingManager.State.Aiming) return;
            _bowlingManager.SetSwingMode(false);
            UpdateModeUI();
            Debug.Log("UIManager: Mode set to Spin");
        }

        private void OnBowlButtonClicked()
        {
            if (_bowlingManager.currentState == BowlingManager.State.Aiming)
            {
                _bowlingManager.StartMetering();
                _meterUI.StartMeter();
            }
            else if (_bowlingManager.currentState == BowlingManager.State.Metering)
            {
                _meterUI.StopMeter();
                float accuracy = _meterUI.GetAccuracy();
                _bowlingManager.Launch(accuracy);
                _meterUI.Hide();
            }
        }

        private void OnSpawnToggleClicked()
        {
            _bowlingManager.ToggleSpawnPoint();
        }

        private void OnResetButtonClicked()
        {
            _bowlingManager.ResetAll();
            _meterUI.Hide();
        }
    }
}
