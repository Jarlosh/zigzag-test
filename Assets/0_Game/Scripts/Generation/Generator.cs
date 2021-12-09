using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _0_Game.Scripts.Generation
{
    public enum GenerationType
    {
        Random, Ordered
    }


    public class Generator : MonoBehaviour
    {
        [SerializeField] private Spawner spawner;
        [SerializeField] private Transform startPosition;
        [SerializeField] private GeneratorConfig config;

        private IGenerationStrategy strategy;
        private float width;

        private Tile lastTile;
        private Vector3 lastPosition;

        private void Start()
        {
            InitStrategy();
            lastPosition = startPosition.position - (Vector3.forward + Vector3.right)/2;   
            Generate();
        }

        private void InitStrategy()
        {
            var min = config.minLength;
            var max = config.maxLength;
            width = config.Width;
            strategy = config.Type switch
            {
                GenerationType.Random => new RandomStrategy(min, max),
                GenerationType.Ordered => new OrderedStrategy(min, max)
            };
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
            
            var primaryStep = looksRight ? Vector3.right : Vector3.forward;
            var secondaryStep = looksRight ? Vector3.forward : Vector3.right;

            lastPosition -= secondaryStep * ((float)(width - 1) / 2);
            if(lastTile != null)
                lastPosition += primaryStep * ((float)(width + 1) / 2);
            for (int i = 0; i < blockInfo.length; i++)
            {
                if(i != 0)
                    lastPosition += primaryStep;
                lastTile = spawner.SetTile(lastPosition, width, looksRight);
                if (blockInfo.collectableIndex == i)
                    spawner.AddCollectable(lastTile, i);
            }
        }
    }
}