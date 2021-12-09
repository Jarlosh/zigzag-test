using System;

namespace _0_Game.Scripts.Management
{
    public class GameState
    {
        public static GameState Instance => GameManager.Instance.State;
        
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
        
        public bool IsStarted { get; set; }

        public event Action<int> OnScoreSetEvent;

        public GameState()
        {
            
        }
    }
}