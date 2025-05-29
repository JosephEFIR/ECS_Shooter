using Unity.Cinemachine;

namespace Project.Scripts.Move
{
    internal struct CameraSwitcherComponent
    {
        public CinemachineCamera firstPersonViewCam;
        public CinemachineCamera thirdPersonViewCam;
        public bool isFPV;
    }
}

