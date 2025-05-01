using System;
using Unity.Cinemachine;

namespace Project.Scripts.Move
{
    [Serializable]
    public struct CameraSwitcherComponent
    {
        public CinemachineCamera firstPersonViewCam;
        public CinemachineCamera thirdPersonViewCam;
    }
}

