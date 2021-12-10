using System;
using _0_Game.Scripts.Zen;
using UnityEngine;
using Zenject;

namespace _0_Game.Scripts.Management.Score
{
    public class MainSceneGUI : MonoBehaviour, IInitializable, IDisposable
    {
        private SignalBus signalBus;
        [SerializeField] private GameObject gameplayGui;
        [SerializeField] private GameObject startInfoWindow;
        [SerializeField] private GameObject gameoverInfoWindow;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }
        
        public void Initialize()
        {
            signalBus.Subscribe<GameOverSignal>(OnGameOver);
            signalBus.Subscribe<GameStartSignal>(OnGameStart);
            startInfoWindow.SetActive(true);
        }

        public void Dispose()
        {
            signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
            signalBus.Unsubscribe<GameStartSignal>(OnGameStart);
        }
        
        private void OnGameOver()
        {
            gameoverInfoWindow.SetActive(true);
        }

        private void OnGameStart()
        {
            startInfoWindow.SetActive(false);
            gameplayGui.SetActive(true);
        }
    }
}