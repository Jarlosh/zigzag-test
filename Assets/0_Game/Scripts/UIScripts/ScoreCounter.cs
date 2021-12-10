using System;
using _0_Game.Scripts.Zen;
using TMPro;
using UnityEngine;
using Zenject;

namespace _0_Game.Scripts.Management.Score
{
    public class ScoreCounter : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private TMP_Text label;
        
        private SignalBus signalBus;
        private ScoreSystem scoreSystem;

        [Inject]
        private void Construct(SignalBus signalBus, ScoreSystem scoreSystem)
        {
            this.signalBus = signalBus;
            this.scoreSystem = scoreSystem;
        }

        private void OnScoreChanged(int old, int updated)
        {
            SetScore(updated);
        }

        private void SetScore(int score)
        {
            label.text = score.ToString();
        }

        public void Initialize()
        {
            scoreSystem.OnScoreChanged += OnScoreChanged;
        }

        public void Dispose()
        {
            scoreSystem.OnScoreChanged -= OnScoreChanged;
        }

        private void OnCollect()
        {
            SetScore(scoreSystem.Score);
        }
    }
}