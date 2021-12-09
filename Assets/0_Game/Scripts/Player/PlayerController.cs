using System;
using _0_Game.Scripts;
using _0_Game.Scripts.Management;
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
            collisionManager.OnCollectableEnterEvent += OnCollectableEnter;
            movement.IsMoving = true;
        }

        private void OnCollectableEnter(Collectable collectable)
        {
            GameState.Instance.Score++;
            collectable.OnCollected();
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