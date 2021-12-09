using UnityEngine;

namespace _0_Game.Scripts.Generation
{
    public class RandomStrategy : BaseStrategy
    {
        private int maxLength = 1;
        
        public RandomStrategy(int maxLength = 5)
        {
            this.maxLength = maxLength;
        }
        
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
            var len = Random.Range(1, maxLength);
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