using UnityEngine;
using System;

namespace DevTest.Service
{
    public class GameManager
    {
        private GameModel _model;
        private PlayerService _playerService;
        private float _deathMagnitude;
        
        public event Action<float, int> OnGameStateUpdate;
        public event Action<string> OnGameOver;

        public GameManager(GameModel model, PlayerService playerService, float deathMagnitude)
        {
            _model = model;
            _playerService = playerService;
            _deathMagnitude = deathMagnitude;
            
            if (CollectionService.Instance != null)
                CollectionService.Instance.OnPointsAdded += HandlePointsAdded;
        }

        public void HandleUpdate()
        {
            if (_model.IsGameOver) return;

            // 1. Timer Logic
            _model.TimeLeft -= Time.deltaTime;
            if (_model.TimeLeft <= 0)
            {
                TriggerGameOver("Time Up!");
            }

            // 2. Infinite Fall Check (Optimized: Use cached playerService)
            if (_playerService != null && _playerService.GetPlayerTransform() != null)
            {
                float currentMag = _playerService.GetPlayerTransform().position.magnitude;
                
                if (currentMag > _deathMagnitude)
                {
                    TriggerGameOver("You fell into the Abyss!");
                }
            }

            // 3. Notify UI
            OnGameStateUpdate?.Invoke(_model.TimeLeft, _model.Score);
        }

        private void HandlePointsAdded(int amount)
        {
            _model.Score += amount;
            _model.CollectedCubes++;
            
            OnGameStateUpdate?.Invoke(_model.TimeLeft, _model.Score);

            // Win Condition
            if (_model.TotalCubes > 0 && _model.CollectedCubes >= _model.TotalCubes)
            {
                TriggerGameOver("You Win! All Cubes Collected.");
            }
        }

        private void TriggerGameOver(string reason)
        {
            _model.IsGameOver = true;
            _model.TimeLeft = 0;
            OnGameOver?.Invoke(reason);
        }
    }
}
