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

        private void Start()
        {
            collisionManager.OnNoSafeSpaceLeftEvent += OnNoSafeSpaceLeftLeft;
            collisionManager.OnCollectableEnterEvent += OnCollectableEnter;
        }

        private void OnDestroy()
        {
            collisionManager.OnNoSafeSpaceLeftEvent -= OnNoSafeSpaceLeftLeft;
            collisionManager.OnCollectableEnterEvent -= OnCollectableEnter;
        }

        private void OnCollectableEnter(Collectable collectable)
        {
            GameState.Instance.Score++;
            collectable.OnCollected();
        }
        
        private void OnNoSafeSpaceLeftLeft()
        {
            GameManager.Instance.Gameover();
        }
    }
}