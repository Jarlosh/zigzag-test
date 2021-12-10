using System;
using _0_Game.Scripts;
using _0_Game.Scripts.Tools;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class PlayerCollisions : IInitializable, IDisposable
    {
        [Serializable]
        public class LayerConfig
        {
            public LayerMask safeLayer;
            public LayerMask collectableLayer;
        }
        
        private SignalBus signalBus;
        private TriggerEvents triggerEvents;
        private LayerConfig config;
        private int stayingSafeSpacesCount;

        public event Action OnNoSafeSpaceLeftEvent;
        public event Action<Collectable> OnCollectableEnterEvent;

        [Inject]
        private void Construct(LayerConfig layerConfig, SignalBus signalBus, TriggerEvents triggerEvents)
        {
            this.config = layerConfig;
            this.signalBus = signalBus;
            this.triggerEvents = triggerEvents;
        }

        public void Initialize()
        {
            triggerEvents.OnTriggerEnterEvent += OnTriggerEnter;
            triggerEvents.OnTriggerExitEvent += OnTriggerExit;
        }

        public void Dispose()
        {
            triggerEvents.OnTriggerEnterEvent -= OnTriggerEnter;
            triggerEvents.OnTriggerExitEvent -= OnTriggerExit;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (IsSafeSpace(other))
                stayingSafeSpacesCount++;
            
            if(HasLayer(other, config.collectableLayer))
                if(other.gameObject.TryGetComponent<Collectable>(out var collectable))
                    OnCollectableEnterEvent?.Invoke(collectable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsSafeSpace(other))
            {
                stayingSafeSpacesCount--;
                if(stayingSafeSpacesCount == 0)
                    OnNoSafeSpaceLeftEvent?.Invoke();
            }
        }

        private bool IsSafeSpace(Collider other)
        {
            return HasLayer(other, config.safeLayer);
        }

        private bool HasLayer(Collider other, LayerMask mask)
        {
            return (mask.Contains(other.gameObject.layer));
        }
    }
}