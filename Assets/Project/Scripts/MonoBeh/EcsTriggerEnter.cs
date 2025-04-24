using UnityEngine;

namespace Project.Scripts.MonoBeh
{
    public class EcsTriggerEnter : MonoBehaviour
    {
        [SerializeField] private string targetTag = "TargetTagName";
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(targetTag))
            {
                //TRIGGER LOGIC
            }
        }
    }
}