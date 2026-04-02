using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace DevTest.Service
{
    public class UIService : GenericSingleton<UIService>
    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _scoreText;
        
        [Header("Game End Panel")]
        [SerializeField] private GameObject _gameEndPanel;
        [SerializeField] private TMP_Text _gameEndMessageText;
        [SerializeField] private Button _restartButton;

        protected override void Awake()
        {
            base.Awake();
            
            // 1. Force the UI into a clean start state
            if (_gameEndPanel != null) _gameEndPanel.SetActive(false);
            
            // 2. Link the Restart logic
            if (_restartButton != null)
                _restartButton.onClick.AddListener(RestartGame);
        }

        public void UpdateUI(float timeLeft, int currentScore)
        {
            if (_timerText != null) _timerText.text = $"Time: {Mathf.CeilToInt(timeLeft)}s";
            if (_scoreText != null) _scoreText.text = $"Score: {currentScore}";
        }

        public void ShowGameEnd(string message)
        {
            if (_gameEndPanel != null) 
            {
                _gameEndPanel.SetActive(true);
            }

            if (_gameEndMessageText != null) _gameEndMessageText.text = message;
        }

        public void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}
