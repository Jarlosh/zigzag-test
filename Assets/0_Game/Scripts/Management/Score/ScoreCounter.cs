using System;
using TMPro;
using UnityEngine;

namespace _0_Game.Scripts.Management.Score
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;

        private void Start()
        {
            GameState.Instance.OnScoreSetEvent += SetScore;
            SetScore(GameState.Instance.Score);
        }

        private void SetScore(int score)
        {
            label.text = score.ToString();
        }
    }
}