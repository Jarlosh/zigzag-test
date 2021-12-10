using System;
using _0_Game.Scripts.Management;
using _0_Game.Scripts.Zen;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Scripts
{
    public class PlayerMovement : IInitializable, IDisposable, ITickable
    {
        [Serializable]
        public class Settings
        {
            public float speed;
            public bool isMovingXOnStart;
        }

        private SignalBus signalBus;
        private PlayerData player;
        
        private float speed = 1;
        private bool isMovingX;
        private Vector3 moveDirection;

        public bool IsMoving { get; private set; } = false;
        public bool IsMovingX 
        {
            get => isMovingX;
            private set
            {
                if (isMovingX == value)
                    return;
                isMovingX = value;
                UpdateDirection();
            }
        }

        [Inject]
        private void Construct(SignalBus signalBus, Settings settings, PlayerData playerData)
        {
            this.signalBus = signalBus;
            this.player = playerData;
            speed = settings.speed;
            isMovingX = settings.isMovingXOnStart;
            UpdateDirection();
        }

        public void Initialize()
        {
            signalBus.Subscribe<GameOverSignal>(OnGameover);
            signalBus.Subscribe<GameStartSignal>(OnGameStarted);
        }

        public void Dispose()
        {
            signalBus.Unsubscribe<GameOverSignal>(OnGameover);
            signalBus.Unsubscribe<GameStartSignal>(OnGameStarted);
        }
        
        private void UpdateDirection()
        {
            moveDirection = IsMovingX ? Vector3.right : Vector3.forward;
        }
        
        private void OnGameStarted()
        {
            IsMoving = true;
        }
        
        private void OnGameover()
        {
            IsMoving = false;
        }

        public void Tick()
        {
            if (!IsMoving)
                return;

            CheckInput();
            Move();
        }
        
        private void CheckInput()
        {
            if(IsSwitchPressed())
                IsMovingX = !IsMovingX;
        }

        private bool IsSwitchPressed()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        private void Move()
        {
            var moveDelta = moveDirection * (speed * Time.fixedDeltaTime);
            player.movedTransform.position += moveDelta;
        }
    }
}