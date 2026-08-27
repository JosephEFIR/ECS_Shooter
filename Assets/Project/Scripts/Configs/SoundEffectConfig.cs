using UnityEngine;

namespace Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "SoundEffectConfig", menuName = "Configs/Sound/SoundEffectConfig")]
    public class SoundEffectConfig : ScriptableObject
    {
        [SerializeField] private AudioClip ambientSoundOne;
        [SerializeField] private AudioClip ambientSoundTwo;
        
        [SerializeField] private AudioClip laserShoot;
        [SerializeField] private AudioClip laserHit;

        public AudioClip AmbientSoundOne => ambientSoundOne;
        public AudioClip AmbientSoundTwo => ambientSoundTwo;
        public AudioClip LaserShoot => laserShoot;
        public AudioClip LaserHit => laserHit;
    }
}