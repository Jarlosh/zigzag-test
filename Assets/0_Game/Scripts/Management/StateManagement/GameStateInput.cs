using System;
using UnityEngine;
using Zenject;

namespace _0_Game.Scripts.Management
{
    public class GameStateInput : ITickable
    {
        private GameState gameState;
        public event Action OnGameStartPressedEvent;
        public event Action OnRestartPressedEvent;
        
        [Inject]
        private void Construct(GameState gameState)
        {
            this.gameState = gameState;
        }
        
        public void Tick()
        {
            if(!gameState.IsStarted)
            {
                if (IsStartPressed())
                    OnGameStartPressedEvent?.Invoke();
            }
            else if (gameState.IsFinished)
            {
                if (IsRestartPressed())
                    OnRestartPressedEvent?.Invoke();
            }
        }
        
        private bool IsRestartPressed()
        {
            return IsSpacePressed();
        }

        private bool IsStartPressed()
        {
            return IsSpacePressed();
        }
        
        private bool IsSpacePressed()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}