using System;
using System.Collections.Generic;
using UnityEngine;

namespace _0_Game.Scripts.Generation
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] private Spawner spawner;
        [SerializeField] private Transform startPosition;
        
        // [SerializeField] private Sight sight;
        private IGenerationStrategy strategy = new TestStrategy();
        private Tile lastTile;
        private Vector3 lastPosition;

        private void Start()
        {
            lastPosition = startPosition.position;   
            Generate();
        }

        public void Generate()
        {
            do AddBlock();
            while (NeedMoreBlocks());
        }

        private void Update()
        {
            if(NeedMoreBlocks())
                AddBlock();
        }

        private bool NeedMoreBlocks()
        {
            return lastTile.IsInSight();
        }

        private void AddBlock()
        {
            var blockInfo = strategy.GetNextBlockInfo();
            var looksRight = blockInfo.looksRight;
            var step = looksRight ? Vector3.right : Vector3.forward;
            
            for (int i = 0; i < blockInfo.length; i++)
            {
                lastPosition += step; 
                lastTile = spawner.SetTile(lastPosition);
                if (blockInfo.collectableIndex == i)
                    spawner.AddCollectable(lastTile, i);
            }
        }
    }
}