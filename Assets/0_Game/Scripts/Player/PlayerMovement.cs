using System;
using _0_Game.Scripts.Management;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private bool isMovingX;

        private Vector3 moveDirection;

        public bool IsMoving { get; private set; } = false;
        public bool IsMovingX 
        {
            get => isMovingX;
            set
            {
                if (isMovingX == value)
                    return;
                isMovingX = value;
                UpdateDirection();
            }
        }

        private void UpdateDirection()
        {
            moveDirection = IsMovingX ? Vector3.right : Vector3.forward;
        }

        private void Start()
        {
            var gm = GameManager.Instance;
            gm.OnGameoverEvent += OnGameover;
            gm.OnGameStartedEvent += OnGameStarted;
            UpdateDirection();
        }
        
        private void OnDestroy()
        {
            var gm = GameManager.Instance;
            if (gm == null)
                return;
            gm.OnGameoverEvent -= OnGameover;
            gm.OnGameStartedEvent -= OnGameStarted;
        }

        private void OnGameStarted()
        {
            IsMoving = true;
        }
        
        private void OnGameover()
        {
            IsMoving = false;
        }

        private void Update()
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
            transform.position += moveDelta;
        }
    }
}