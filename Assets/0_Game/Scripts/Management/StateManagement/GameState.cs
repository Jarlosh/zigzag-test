using System;

namespace _0_Game.Scripts.Management
{
    [Serializable]
    public class GameState
    {
        public bool IsStarted { get; set; } = false;
        public bool IsFinished { get; set; } = false;

        public void Reset()
        {
            IsStarted = IsFinished = false;
        }
    }
}