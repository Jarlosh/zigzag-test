using UnityEngine;

namespace _0_Game.Scripts.Management
{
    public class GameStateInput : MonoBehaviour
    {
        public GameState state => GameState.Instance;
        public GameManager gm => GameManager.Instance;

        private void Update()
        {
            if(!state.IsStarted)
            {
                if (IsStartPressed())
                    gm.StartGame();
            }
            else if (state.IsFinished)
            {
                if (IsRestartPressed())
                    gm.Restart();
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