using UnityEngine;

namespace _0_Game.Scripts.Generation
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField] Transform startPositionSource;
        public Transform tileContainer;

        public Vector3 StartPosition => startPositionSource.position;
    }
}