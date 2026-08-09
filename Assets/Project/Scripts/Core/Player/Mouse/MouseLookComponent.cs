using UnityEngine;

namespace Project.Scripts.Move
{
    internal struct MouseLookComponent
    {
        public Vector3 Direction;
        
        public float Sensitivity;
        public Camera Camera; //TODO Параметр градусов в сек
    }
}