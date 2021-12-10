using System;
using _0_Game.Scripts.Zen;
using Zenject;

namespace _0_Game.Scripts.Management.Score
{
    public delegate void ValueChangedDelegate<T>(T old, T updated);

    public class ScoreSystem : IInitializable, IDisposable
    {
        private SignalBus signalBus;
        private int score = 0;

        public int Score
        {
            get => score;
            set
            {
                if (score == value)
                    return;
                var old = score;
                score = value;
                OnScoreChanged?.Invoke(old, score);
            }
        }

        public event ValueChangedDelegate<int> OnScoreChanged;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }
        
        public void Initialize()
        {
            signalBus.Subscribe<ItemCollectedSignal>(OnCollect);
        }

        public void Dispose()
        {
            signalBus.Unsubscribe<ItemCollectedSignal>(OnCollect);
        }

        private void OnCollect()
        {
            Score++;
        }
    }
}