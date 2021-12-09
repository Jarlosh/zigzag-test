using System;
using _0_Game.Scripts;
using _0_Game.Scripts.Tools;
using UnityEngine;

namespace Scripts
{
    public class PlayerCollisions : MonoBehaviour
    {
        [SerializeField] private LayerMask safeLayer;
        [SerializeField] private LayerMask collectableLayer;
        
        public event Action OnNoSafeSpaceLeftEvent;
        public event Action<Collectable> OnCollectableEnterEvent;

        private int stayingSafeSpacesCount;
        
        private void OnTriggerEnter(Collider other)
        {
            if (IsSafeSpace(other))
                stayingSafeSpacesCount++;
            
            if(HasLayer(other, collectableLayer))
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

        private bool HasLayer(Collider other, LayerMask mask)
        {
            return (mask.Contains(other.gameObject.layer));
        }
        
        private bool IsSafeSpace(Collider other)
        {
            return HasLayer(other, safeLayer);
        }
    }
}