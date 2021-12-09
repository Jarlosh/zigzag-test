using System;
using _0_Game.Scripts.Tools;
using UnityEngine;

namespace Scripts
{
    public class PlayerCollisions : MonoBehaviour
    {
        [SerializeField] private LayerMask safeLayer;
        
        public event Action OnNoSafeSpaceLeftEvent;

        private int stayingSafeSpacesCount;
        
        private void OnTriggerEnter(Collider other)
        {
            if (IsSafeSpace(other))
                stayingSafeSpacesCount++;
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
            return (safeLayer.Contains(other.gameObject.layer));
        }
    }
}