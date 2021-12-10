using System;

namespace _0_Game.Scripts.Management
{
    public class GameState
    {
        public static GameState Instance => GameManager.Instance.State;
        
        public bool IsStarted { get; set; } = false;
        public bool IsFinished { get; set; } = false;
        private int score;

        public int Score
        {
            get => score;
            set
            {
                score = value;
                OnScoreSetEvent?.Invoke(score);
            }
        }

        public event Action<int> OnScoreSetEvent;
    }
}