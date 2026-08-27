using UnityEngine;

namespace Project.Scripts.Core.Common
{
    public class LookAtPlayer : MonoBehaviour
    {
        private void Update()
        {
            if(Camera.main is null) return;
            
            Vector3 direction = transform.position - Camera.main.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 300 * Time.deltaTime);
        }
    }
}