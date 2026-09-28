using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Level
{
    public class LevelStaticBatcher : MonoBehaviour
    {
        [SerializeField] private Transform levelRoot;
        private string dynamicTag = "Turret";
        
        private void Start()
        {
            if (levelRoot == null) levelRoot = transform;
            Invoke(nameof(Combine), 0.5f);
        }
        
        private void Combine()
        {
            List<(Transform turret, Transform originalParent)> detached = new();
            
            Transform[] allChildren = levelRoot.GetComponentsInChildren<Transform>(true);
            
            foreach (var t in allChildren)
            {
                if (t.CompareTag(dynamicTag))
                {
                    detached.Add((t, t.parent));
                    t.SetParent(null, true);
                }
            }
            
            StaticBatchingUtility.Combine(levelRoot.gameObject);
            
            foreach (var (turret, originalParent) in detached)
            {
                if (originalParent != null && turret != null)
                {
                    turret.SetParent(originalParent, true);
                }
            }
            
            Debug.Log($"Level static batching combined, detached {detached.Count} dynamic objects");
        }
    }
}