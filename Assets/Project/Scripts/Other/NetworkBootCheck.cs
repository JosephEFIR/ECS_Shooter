using Mirror;
using UnityEngine;

namespace Project.Scripts.Other
{
    public class NetworkBootCheck : MonoBehaviour
        {
            private void Start()
            {
                Debug.Log($"[BootCheck] singleton = {(NetworkManager.singleton != null ? "OK" : "NULL")}");
            }
    
            private void Update()
            {
                if (Input.GetKeyDown(KeyCode.H))
                {
                    NetworkManager.singleton.StartHost();
                    Debug.Log("[BootCheck] StartHost");
                }
    
                if (Input.GetKeyDown(KeyCode.C))
                {
                    NetworkManager.singleton.StartClient();
                    Debug.Log("[BootCheck] StartClient");
                }
            }
    
            private void OnGUI()
            {
                if (GUILayout.Button("HOST", GUILayout.Width(200), GUILayout.Height(50)))
                {
                    NetworkManager.singleton.StartHost();
                }
                if (GUILayout.Button("CLIENT", GUILayout.Width(200), GUILayout.Height(50)))
                {
                    NetworkManager.singleton.StartClient();
                }
            }
        }
}