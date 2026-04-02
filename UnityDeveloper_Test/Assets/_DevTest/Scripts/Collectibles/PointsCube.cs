using UnityEngine;
using DevTest.Service;

namespace DevTest.Collectibles
{
    [RequireComponent(typeof(BoxCollider))]
    public class PointsCube : MonoBehaviour
    {
        [SerializeField] private int _pointsValue = 10;


        private void OnTriggerEnter(Collider other)
        {
            // Simple check: Is this the player?
            if (other.GetComponent<Player.PlayerView>() != null)
            {
                if (CollectionService.Instance != null)
                {
                    CollectionService.Instance.AddPoints(_pointsValue);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
