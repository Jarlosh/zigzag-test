using UnityEngine;

namespace _0_Game.Scripts.Generation
{
    public class RandomStrategy : BaseStrategy
    {
        public RandomStrategy(int min, int max) : base(min, max) { }

        protected override BlockInfo MakeFirstInfo()
        {
            var lookRight = Random.value > 0.5f;
            return MakeRandom(lookRight);
        }

        protected override BlockInfo MakeNextBlockInfo()
        {
            return MakeRandom(!lastInfo.looksRight);
        }

        private BlockInfo MakeRandom(bool looksRight)
        {
            var len = PickLength();
            var colIndex = Random.Range(0, len - 1);
            return new BlockInfo()
            {
                length = len,
                collectableIndex = colIndex,
                looksRight = looksRight
            };
        }
    }
}