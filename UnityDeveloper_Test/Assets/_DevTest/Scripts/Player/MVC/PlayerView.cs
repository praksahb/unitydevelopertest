using UnityEngine;

namespace DevTest.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider), typeof(PlayerAnimationController))]
    public class PlayerView : MonoBehaviour
    {
        public PlayerController PlayerController { get; set; }

        private PlayerAnimationController _animController;
        public PlayerAnimationController PlayerAnimController => _animController;

        [SerializeField] private float groundCheckDistance = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        private Rigidbody _rb;
        public Rigidbody Rigidbody => _rb;

        [SerializeField] private GameObject _hologram;
        public GameObject Hologram => _hologram;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _animController = GetComponent<PlayerAnimationController>();
        }

        public bool IsGrounded
        {
            get
            {
                // Simple sphere cast to check for ground
                return Physics.SphereCast(transform.position + Vector3.up * 0.1f, 0.1f, Vector3.down, out _, groundCheckDistance, groundLayer);
            }
        }
    }
}
