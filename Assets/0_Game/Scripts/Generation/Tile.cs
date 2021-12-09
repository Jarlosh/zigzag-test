using System;
using _0_Game.Scripts.Tools;
using UnityEngine;

namespace _0_Game.Scripts.Generation
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private LayerMask sightTriggerMask;
        public int Width { get; private set; }
        
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public event Action<GameObject> OnSightTriggerEnter;
        public event Action<GameObject> OnSightTriggerExit;
        public event Action<Tile> OnDestroyEvent;

        private void OnTriggerEnter(Collider other)
        {
            if(sightTriggerMask.Contains(other.gameObject.layer))
                OnSightTriggerEnter?.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if(sightTriggerMask.Contains(other.gameObject.layer))
                OnSightTriggerExit?.Invoke(other.gameObject);
        }

        private void OnDestroy()
        {
            OnDestroyEvent?.Invoke(this);
        }

        public bool IsInSight()
        {
            return Physics.CheckBox(Position, transform.lossyScale / 2, transform.rotation, sightTriggerMask);
        }
    }
}