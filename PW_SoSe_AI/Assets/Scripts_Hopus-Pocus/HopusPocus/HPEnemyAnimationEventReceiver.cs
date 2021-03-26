using System;
using Core.Utility;
using UnityEngine;

namespace AISystem
{
    /// <summary>
    /// 	Class that receives various animation events and forwards them. All objects can listen to these events that way.
    /// 	Simple way to propagate animation events from a to b.
    /// </summary>
    public class HPEnemyAnimationEventReceiver : CachedMonoBehaviour
    {
        // all these get called in some of the animations. Select CagneyCarnationBoss -> GraphicsObejct and check its animations for how its done & setup
        public event Action<StateIdentifier> OnAnimationDone;
        public event Action OnSpawnGatlingProjectile;
        public static event Action OnSpawnMagicHandsProjectile;
        public static event Action OnSpawnSpitProjectile;
        public static event Action OnMiniFlowerShoot;

        private AudioSource _source;

        protected override void Awake()
        {
            base.Awake();
            _source = GetComponent<AudioSource>();
        }

        private void AnimationDone(AnimationEvent animEvent)
        {
            StateIdentifier id = (StateIdentifier)animEvent.objectReferenceParameter;
            OnAnimationDone?.Invoke(id);
        }

        private void SpawnMagicHandsProjectile()
        {
            OnSpawnMagicHandsProjectile?.Invoke();
        }

        private void SpawnSpitProjectile()
        {
            OnSpawnSpitProjectile?.Invoke();
        }

        private void SpawnGatlingProjectile()
        {
            OnSpawnGatlingProjectile?.Invoke();
        }

        private void SpawnMiniFlowerProjectile()
        {
            OnMiniFlowerShoot?.Invoke();
        }

        private void PlayAudio(AnimationEvent animationEvent)
        {
            AudioClip clip = (AudioClip)animationEvent.objectReferenceParameter;

            _source.clip = clip;
            _source.Play();
        }
    }
}
