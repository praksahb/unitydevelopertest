using UnityEngine;

namespace DevTest.Player
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
    public class PlayerSO : ScriptableObject
    {
        [Header("Movement Settings")]
        public float Speed = 5f;
        public float JumpForce = 5f;
        public float GravityForce = -9.81f;
        
        [Header("References")]
        public PlayerView PlayerPrefab;
    }
}
