using UnityEngine;

namespace Project.Scripts.Other
{
    public class TimeScalerDebug : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) Time.timeScale = 1f;
            if (Input.GetKeyDown(KeyCode.Alpha2)) Time.timeScale = 0.25f;
            if (Input.GetKeyDown(KeyCode.Alpha3)) Time.timeScale = 0.1f;
        }
    }
}