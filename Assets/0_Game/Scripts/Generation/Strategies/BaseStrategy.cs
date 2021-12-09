using UnityEngine;

namespace _0_Game.Scripts.Generation
{
    public abstract class BaseStrategy : IGenerationStrategy
    {
        private bool firstBlock;
        protected BlockInfo lastInfo;
        
        protected int minLength = 1;
        protected int maxLength = 1;

        public BaseStrategy(int minLength = 1, int maxLength = 5)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public int PickLength() => Random.Range(minLength, maxLength);
        
        public BlockInfo GetNextBlockInfo()
        {
            return lastInfo = firstBlock ? MakeFirstInfo() : MakeNextBlockInfo();
        }

        protected abstract BlockInfo MakeFirstInfo();
        protected abstract BlockInfo MakeNextBlockInfo();
    }
}