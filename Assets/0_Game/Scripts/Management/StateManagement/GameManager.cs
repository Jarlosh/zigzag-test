using System;
using System.Collections;
using System.Threading.Tasks;
using _0_Game.Scripts.Tools.CoroutineHelper;
using _0_Game.Scripts.Zen;
using Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _0_Game.Scripts.Management
{
    public class GameManager : IInitializable, IDisposable
    {
        private SignalBus signalBus;
        private GameState state;
        private GameStateInput stateInput;

        private bool loadingScene = false;

        [Inject]
        private void Construct(SignalBus signalBus, GameState gameState, GameStateInput stateInput)
        {
            this.signalBus = signalBus;
            this.state = gameState;
            this.stateInput = stateInput;
        }

        public void Initialize()
        {
            signalBus.Subscribe<DeathSignal>(Gameover);
            stateInput.OnGameStartPressedEvent += StartGame;
            stateInput.OnRestartPressedEvent += Restart;
            state.Reset();
        }
        
        public void Dispose()
        {
            signalBus.Unsubscribe<DeathSignal>(Gameover);
            stateInput.OnGameStartPressedEvent -= StartGame;
            stateInput.OnRestartPressedEvent -= Restart;
        }

        private void OnSceneLoaded()
        {
            state.Reset();
        }

        public void StartGame()
        {
            state.IsStarted = true;
            signalBus.Fire<GameStartSignal>();
        }

        public void Gameover()
        {
            state.IsFinished = true;
            signalBus.Fire<GameOverSignal>();
        }
        
        public void Restart()
        {
            if (loadingScene)
                return;
            loadingScene = true;
            
            //todo: implement reset without scene reload
            var task = ReloadScene();
            task.completed += OnSceneLoaded;
        }

        private void OnSceneLoaded(AsyncOperation loadTask)
        {
            loadTask.completed -= OnSceneLoaded;
            OnSceneLoaded();
            loadingScene = false;
        }

        private static AsyncOperation ReloadScene()
        {
            return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

    }
}