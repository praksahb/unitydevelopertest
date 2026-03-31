using UnityEngine;

namespace DevTest.Player
{
    public class PlayerView : MonoBehaviour
    {
        public PlayerController PlayerController { get; set; }


        [SerializeField] private Animator _animator;
    }
}
