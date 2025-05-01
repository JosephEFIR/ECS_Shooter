using Leopotam.Ecs;

namespace Project.Scripts.Move
{
    sealed class CameraSwitcherSystem : IEcsInitSystem,IEcsRunSystem
    {
        private readonly EcsFilter<CameraSwitcherComponent, CameraSwitchEvent> _cameraSwitchFilter = null;
        private readonly EcsFilter<CameraSwitcherComponent> _camerasFilter = null;
        
        public void Init()
        {
            foreach (var i in _camerasFilter)
            {
                ref var fpvCamera = ref _cameraSwitchFilter.Get1(i).firstPersonViewCam;
                fpvCamera.gameObject.SetActive(true);
            }
        }
        
        public void Run()
        {
            foreach (var i in _cameraSwitchFilter)
            {
                ref var fpvCamera = ref _cameraSwitchFilter.Get1(i).firstPersonViewCam;
                ref var tpvCamera = ref _cameraSwitchFilter.Get1(i).thirdPersonViewCam;

                if (fpvCamera.gameObject.activeInHierarchy)
                {
                    tpvCamera.gameObject.SetActive(true);
                    fpvCamera.gameObject.SetActive(false);
                }
                else
                {
                    tpvCamera.gameObject.SetActive(false);
                    fpvCamera.gameObject.SetActive(true);
                }
            }
        }
    }
}