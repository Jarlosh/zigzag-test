using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _0_Game.Scripts.Generation
{
    public enum GenerationStrategy
    {
        Random, Ordered
    }

    public class Generator : IInitializable, ITickable
    {
        private GeneratorConfig config;
        private LevelData levelData;
        private IGenerationStrategy strategy;
        
        private Tile.Factory tilePool;
        private Collectable.Factory collectablePool;
        
        private Tile lastTile;
        private Vector3 lastPosition;

        [Inject]
        private void Construct(
            GeneratorConfig config, LevelData levelData, 
            IGenerationStrategy strategy, 
            Tile.Factory tilePool, Collectable.Factory collectablePool)
        {
            this.config = config;
            this.levelData = levelData;
            this.strategy = strategy;
            this.tilePool = tilePool;
            this.collectablePool = collectablePool;
        }
        
        public void Initialize()
        {
            lastPosition = levelData.StartPosition;// - (Vector3.forward + Vector3.right)*(width/2);   
            Generate();
        }

        public void Generate()
        {
            do AddBlock();
            while (NeedMoreBlocks());
        }
        
        public void Tick()
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
            var width = config.Width;
            
            var primaryStep = looksRight ? Vector3.right : Vector3.forward;
            var secondaryStep = looksRight ? Vector3.forward : Vector3.right;
            
            if(lastTile != null)
            {
                lastPosition -= secondaryStep * (((float)width - 1) / 2);
                lastPosition += primaryStep * (((float)width + 1) / 2);
            }
            else
            {
                lastPosition -= secondaryStep * ((float)width / 2);
                lastPosition += primaryStep / 2;
            }

            var sideOffset = secondaryStep * ((float)width - 1) / 2;
            for (int i = 0; i < blockInfo.length; i++)
            {
                if(i != 0)
                    lastPosition += primaryStep;
                
                for (int j = 0; j < width; j++)
                {
                    var position = lastPosition - sideOffset + j * secondaryStep;
                    lastTile = SpawnTile(position, 1, looksRight);
                }
                if (blockInfo.collectableIndex == i)
                    SpawnCollectable(lastTile);
            }
        }

        private Collectable SpawnCollectable(Tile parent)
        {
            return collectablePool.Create(parent);
        }

        private Tile SpawnTile(Vector3 position, int width, bool looksRight)
        {
            return tilePool.Create(position, width, looksRight);
        }
    }
}