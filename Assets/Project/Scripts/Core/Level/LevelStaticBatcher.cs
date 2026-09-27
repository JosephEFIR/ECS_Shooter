using UnityEngine;

namespace Project.Scripts.Level
{
    public class LevelStaticBatcher : MonoBehaviour
    {
        [SerializeField] private Transform levelRoot;
        
        private void Start()
        {
            if (levelRoot == null) levelRoot = transform;
            
            Invoke(nameof(Combine), 0.5f);
        }
        
        private void Combine()
        {
            StaticBatchingUtility.Combine(levelRoot.gameObject);
            Debug.Log("Level static batching combined");
        }
    }
}