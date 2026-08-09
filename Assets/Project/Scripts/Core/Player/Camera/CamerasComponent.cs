using Unity.Cinemachine;
using UnityEngine;

namespace Project.Scripts.Move
{
    internal struct CamerasComponent
    {
        public CinemachineCamera firstPersonViewCam;
        public CinemachineCamera thirdPersonViewCam;
        public bool isFPV;
    }
}

