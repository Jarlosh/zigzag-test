using System;
using _0_Game.Scripts.Generation;
using UnityEngine;
using Zenject;

namespace _0_Game.Scripts
{
    public class Collectable : MonoBehaviour, IPoolable<Tile, IMemoryPool>, IDisposable
    {
        private IMemoryPool pool;

        public void OnCollected()
        {
            Dispose();
        }
        public void OnDespawned()
        {
            
        }

        public void OnSpawned(Tile ownerTile, IMemoryPool pool)
        {
            transform.parent = ownerTile.transform;
            transform.position = ownerTile.transform.position + Vector3.up;
            this.pool = pool;
        }

        public void Dispose()
        {
            pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<Tile, Collectable> { }
    }
}