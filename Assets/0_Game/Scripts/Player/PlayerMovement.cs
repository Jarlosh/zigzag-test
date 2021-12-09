using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private bool isMovingX;

        private Vector3 moveDirection;

        public bool IsMoving { get; set; }
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
            UpdateDirection();
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