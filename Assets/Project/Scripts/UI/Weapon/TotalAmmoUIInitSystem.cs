using Leopotam.Ecs;
using Project.Scripts.Core.Player;
using Project.Scripts.Tags;
using Project.Scripts.Weapon;
using UniRx;
using System;

namespace Project.Scripts.UI.Weapon
{
    public class TotalAmmoUIInitSystem : IEcsRunSystem, IEcsDestroySystem
    {
        private readonly UiView _uiView = null;
        private readonly EcsFilter<PlayerComponent, WeaponInventoryComponent, InitializedEvent> _filter = null;
        
        private CompositeDisposable _disposable = new();
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var inventory = ref _filter.Get2(i);
                ref var weaponComponent = ref inventory.CurrentWeapon.Get<WeaponComponent>();
                
                var magazineSize = weaponComponent.MagazineSize;
                var totalAmmo = weaponComponent.TotalAmmo;
        
                Observable.CombineLatest(magazineSize, totalAmmo, (mag, total) => 
                    {
                        int magDisplay = Math.Min(mag, total);
                        int reserveDisplay = Math.Max(0, total - magDisplay);
                        
                        return $"{magDisplay}/{reserveDisplay}";
                    })
                    .Subscribe(text => 
                    {
                        _uiView.TotalAmmoView.TextMeshPro.text = text;
                    })
                    .AddTo(_disposable);
            }
        }

        public void Destroy()
        {
            _disposable?.Clear();
            _disposable?.Dispose();
        }
    }
}