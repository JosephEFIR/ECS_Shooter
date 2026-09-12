using Unity.AI.Navigation;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.AI
{
    public class PatrolRoomPointsContainer : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        public Transform[] Points => points;

        private void Awake()
        {
            var surface = GetComponent<NavMeshSurface>();
            if (surface != null) surface.BuildNavMesh();
        }
    }
}