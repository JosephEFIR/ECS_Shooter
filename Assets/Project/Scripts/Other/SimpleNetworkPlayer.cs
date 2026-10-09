using Mirror;
using UnityEngine;

namespace Project.Scripts.Other
{
    public class SimpleNetworkPlayer : NetworkBehaviour
    {
        [SerializeField] private float speed = 5f;

        private void Update()
        {
            if (!isLocalPlayer) return;

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(h, 0, v) * speed * Time.deltaTime;
            transform.Translate(move);
        }
    }
}