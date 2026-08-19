using Unity.Cinemachine;

namespace Project.Scripts.Move
{
    internal struct CamerasComponent
    {
        public CinemachineCamera firstPersonViewCam;
        public CinemachineCamera thirdPersonViewCam;
        public bool isFPV;
    }
}

