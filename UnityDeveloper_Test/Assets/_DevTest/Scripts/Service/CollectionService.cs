using UnityEngine;
using DevTest.Collectibles;

namespace DevTest.Service
{
    public class CollectionService : GenericSingleton<CollectionService>
    {
        [SerializeField] private PointsCube[] _cubes;

        public int TotalCubes 
        {
            get 
            {
                return _cubes != null ? _cubes.Length : 0;
            }
        }

        public event System.Action<int> OnPointsAdded;

        public void AddPoints(int amount)
        {
            OnPointsAdded?.Invoke(amount);
        }
    }
}
