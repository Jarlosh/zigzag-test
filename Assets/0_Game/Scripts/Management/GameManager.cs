using System;
using System.Collections;
using System.Threading.Tasks;
using Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _0_Game.Scripts.Management
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        public GameState State { get; set; }
        private bool loadingScene = false;
        
        public event Action OnGameStartedEvent;
        public event Action OnGameoverEvent;
        
        protected override void OnAwake()
        {  
            OnSceneLoaded();
        }

        private void OnSceneLoaded()
        {
            State = new GameState();
        }

        private void OnSceneUnload()
        {
        }

        public void StartGame()
        {
            State.IsStarted = true;
            OnGameStartedEvent?.Invoke();
        }

        public void Gameover()
        {
            State.IsFinished = true;
            OnGameoverEvent?.Invoke();
        }
        
        public void Restart()
        {
            if (loadingScene)
                return;
            
            //todo: implement reset without scene reload
            OnSceneUnload();
            StartCoroutine(ReloadScene(OnSceneLoaded));
        }

        private IEnumerator ReloadScene(Action callback)
        {
            loadingScene = true;
            var task = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            yield return new WaitUntil(() => task.isDone);
            loadingScene = false;
            callback();
        }

    }
}