using UnityEngine;

namespace _0_Game.Scripts.Management
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        public GameState State { get; set; }
        
        protected override void OnAwake()
        {
            State = new GameState();
        }
    }
}