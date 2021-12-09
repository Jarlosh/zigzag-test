using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerCollisions collisionManager;
        [SerializeField] private PlayerMovement movement;
        
        private bool isAlive;
        public bool IsAlive
        {
            get => isAlive;
            private set
            {
                isAlive = value;
                if(!isAlive)
                    OnDead();
            }
        }
        
        private void Start()
        {
            collisionManager.OnNoSafeSpaceLeftEvent += OnNoSafeSpaceLeftLeft;
            movement.IsMoving = true;
        }
        
        private void OnDead()
        {
            movement.IsMoving = false;
        }

        private void OnNoSafeSpaceLeftLeft()
        {
           IsAlive = false;
        }
    }
}