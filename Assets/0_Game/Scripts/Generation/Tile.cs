using System;
using System.Collections;
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

        public void SetWidth(float width, bool lookRight)
        {
            transform.localScale = lookRight ? new Vector3(1, 1, width) : new Vector3(width, 1, 1);
        }


        private void OnTriggerExit(Collider other)
        {
            if (sightTriggerMask.Contains(other.gameObject.layer))
                StartCoroutine(Fall());
        }

        // too lazy to import dotween
        private IEnumerator Fall()
        {
            while (transform.position.y > -5)
            {
                transform.position += Vector3.down * (5 * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            Destroy(gameObject);
        }

        public bool IsInSight()
        {
            return Physics.CheckBox(Position, transform.lossyScale / 2, transform.rotation, sightTriggerMask);
        }
    }
}