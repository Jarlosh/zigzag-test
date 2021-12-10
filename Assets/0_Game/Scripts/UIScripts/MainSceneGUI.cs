using System;
using UnityEngine;

namespace _0_Game.Scripts.Management.Score
{
    public class MainSceneGUI : MonoBehaviour
    {
        [SerializeField] private GameObject gameplayGui;
        [SerializeField] private GameObject startInfoWindow;
        [SerializeField] private GameObject gameoverInfoWindow;

        private void Start()
        {
            startInfoWindow.SetActive(true);
            GameManager.Instance.OnGameStartedEvent += OnGameStart;
            GameManager.Instance.OnGameoverEvent += OnGameOver;
        }
        
        private void OnDestroy()
        {
            if (GameManager.Instance == null)
                return;
            GameManager.Instance.OnGameStartedEvent -= OnGameStart;
            GameManager.Instance.OnGameoverEvent -= OnGameOver;
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