using System;
using UnityEngine;
using Zenject;

namespace _0_Game.Scripts.Generation
{
    public class GenerationInstaller : MonoInstaller
    {
        [SerializeField] private GeneratorConfig generationConfig;
        [SerializeField] private SpawnConfig spawnConfig;
        [SerializeField] private LevelData levelData;
        
        [Serializable]
        public class SpawnConfig
        {
            public GameObject tilePrefab;
            public GameObject collectablePrefab;
        }
        
        public override void InstallBindings()
        {
            BindPools();
            var strategy = PickStrategy();
            Container.BindInterfacesTo<Generator>()
                .AsSingle()
                .WithArguments(levelData, generationConfig, strategy)
                .NonLazy();
        }

        private void BindPools()
        {
            Container
                .BindFactory<Vector3, float, bool, Tile, Tile.Factory>()
                .FromMonoPoolableMemoryPool(
                    x => x.WithInitialSize(10)
                        .FromComponentInNewPrefab(spawnConfig.tilePrefab)
                        .UnderTransform(levelData.tileContainer));

            Container
                .BindFactory<Tile, Collectable, Collectable.Factory>()
                .FromMonoPoolableMemoryPool(
                    x => x.WithInitialSize(5)
                        .FromComponentInNewPrefab(spawnConfig.collectablePrefab));
        }

        private IGenerationStrategy PickStrategy()
        {
            var config = generationConfig;
            var min = config.minLength;
            var max = config.maxLength;
            return config.Strategy switch
            {
                GenerationStrategy.Random => new RandomStrategy(min, max),
                GenerationStrategy.Ordered => new OrderedStrategy(min, max)
            };
        }
    }
}