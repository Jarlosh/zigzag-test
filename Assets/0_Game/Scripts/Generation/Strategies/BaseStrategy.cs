namespace _0_Game.Scripts.Generation
{
    public abstract class BaseStrategy : IGenerationStrategy
    {
        private bool firstBlock;
        protected BlockInfo lastInfo;
        
        public BlockInfo GetNextBlockInfo()
        {
            return lastInfo = firstBlock ? MakeFirstInfo() : MakeNextBlockInfo();
        }

        protected abstract BlockInfo MakeFirstInfo();
        protected abstract BlockInfo MakeNextBlockInfo();
    }
}