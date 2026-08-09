using Leopotam.Ecs;

namespace Project.Scripts.Move
{
    sealed class CameraSwitcherSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CamerasComponent, CameraSwitchEvent> _cameraSwitchFilter = null;
        private readonly EcsFilter<CamerasComponent> _camerasFilter = null;
        
        public void Run()
        {
            foreach (var i in _cameraSwitchFilter)
            {
                ref var cameraSwitchComponent = ref _cameraSwitchFilter.Get1(i);
                ref var fpvCamera = ref cameraSwitchComponent.firstPersonViewCam;
                ref var tpvCamera = ref cameraSwitchComponent.thirdPersonViewCam;

                if (fpvCamera.gameObject.activeInHierarchy)
                {
                    tpvCamera.gameObject.SetActive(true);
                    fpvCamera.gameObject.SetActive(false);
                    cameraSwitchComponent.isFPV = false;
                }
                else
                {
                    tpvCamera.gameObject.SetActive(false);
                    fpvCamera.gameObject.SetActive(true);
                    cameraSwitchComponent.isFPV = true;
                }
            }
        }
    }
}