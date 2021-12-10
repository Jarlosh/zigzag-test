using System;
using _0_Game.Scripts;
using _0_Game.Scripts.Management;
using _0_Game.Scripts.Zen;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Scripts
{
    public class PlayerController : IInitializable, IDisposable
    {
        private PlayerCollisions collisionManager;
        private SignalBus signalBus;

        [Inject]
        private void Construct(SignalBus signalBus, PlayerCollisions collisionManager)
        {
            this.signalBus = signalBus;
            this.collisionManager = collisionManager;
        }
        
        public void Initialize()
        {
            collisionManager.OnNoSafeSpaceLeftEvent += OnNoSafeSpaceLeft;
            collisionManager.OnCollectableEnterEvent += OnCollectableEnter;
        }

        public void Dispose()
        {
            collisionManager.OnNoSafeSpaceLeftEvent -= OnNoSafeSpaceLeft;
            collisionManager.OnCollectableEnterEvent -= OnCollectableEnter;
        }

        private void OnCollectableEnter(Collectable collectable)
        {
            collectable.OnCollected();
            signalBus.Fire<ItemCollectedSignal>();
        }
        
        private void OnNoSafeSpaceLeft()
        {
            signalBus.Fire<DeathSignal>();
        }
    }
}