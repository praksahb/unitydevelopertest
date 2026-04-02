using DevTest.Player.Service;
using UnityEngine;

namespace DevTest
{
    public class PlayerModel
    {
        // pvt variables
        private float _speed;
        private float _jumpForce;
        private float _gravityForce;

        // public properties
        public float Speed => _speed;
        public float JumpForce => _jumpForce;
        public float GravityForce => _gravityForce;

        // constructor
        public PlayerModel(PlayerSO playerData)
        {
            _speed = playerData.Speed;
            _jumpForce = playerData.JumpForce;
            _gravityForce = playerData.GravityForce;
        }
    }
}
