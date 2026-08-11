using Leopotam.Ecs;
using Project.Scripts.Tags;
using UnityEngine;

public class MousePositionSystem : IEcsRunSystem
{
    private readonly EcsFilter<PlayerComponent> _filter;
    
    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var player = ref _filter.Get1(i);
            ref var camera = ref player.camera;
            ref var aimTarget = ref player.AimTarget;
            
            Vector3 center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            Ray ray = camera.ScreenPointToRay(center);
            
            int layerMask = ~LayerMask.GetMask("Player", "Weapon");
            
            if (Physics.Raycast(ray, out var hit, 1000f, layerMask))
            {
                aimTarget.position = hit.point;
            }
            else aimTarget.position = ray.GetPoint(1000f);
            
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 0.1f);
        }
    }
}