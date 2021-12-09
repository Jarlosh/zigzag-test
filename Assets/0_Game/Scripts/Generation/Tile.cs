using System;
using _0_Game.Scripts.Tools;
using UnityEngine;

namespace _0_Game.Scripts.Generation
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private LayerMask sightTriggerMask;
        
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public event Action<GameObject> OnSightTriggerEnter;
        public event Action<GameObject> OnSightTriggerExit;
        public event Action<Tile> OnDestroyEvent;

        public void SetWidth(float width, bool lookRight)
        {
            transform.localScale = lookRight ? new Vector3(1, 1, width) : new Vector3(width, 1, 1);
        }
        
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