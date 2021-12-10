using System;
using System.Collections;
using _0_Game.Scripts.Tools;
using ModestTree;
using UnityEngine;
using Zenject;

namespace _0_Game.Scripts.Generation
{
    public class Tile : MonoBehaviour, IPoolable<Vector3, float, bool, IMemoryPool>, IDisposable
    {
        private IMemoryPool pool;
        [SerializeField] private LayerMask sightTriggerMask;
        [SerializeField] private Renderer renderer;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        
        public void OnDespawned()
        {
            pool = null;
            // renderer.enabled = false;
        }

        public void OnSpawned(Vector3 position, float width, bool lookRight, IMemoryPool pool)
        {
            Position = position;
            SetWidth(width, lookRight);
            this.pool = pool;
            Assert.IsNotNull(pool);
        }

        public void Dispose()
        {
            if(pool != null)
                pool.Despawn(this);
            else Destroy(gameObject);
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
            Dispose();
        }

        public bool IsInSight()
        {
            return Physics.CheckBox(Position, transform.lossyScale / 2, transform.rotation, sightTriggerMask);
        }

        public class Factory : PlaceholderFactory<Vector3, float, bool, Tile>
        {
        }
    }
}