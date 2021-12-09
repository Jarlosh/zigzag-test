namespace _0_Game.Scripts.Generation
{
    public class TestStrategy : IGenerationStrategy
    {
        private bool firstBlock;
        private BlockInfo lastInfo;
        
        public BlockInfo GetNextBlockInfo()
        {
            return lastInfo = firstBlock ? MakeFirstInfo() : MakeNextBlockInfo();
        }

        private BlockInfo MakeNextBlockInfo()
        {
            return new BlockInfo()
            {
                collectableIndex = 0,
                length = 3,
                looksRight = !lastInfo.looksRight
            };
        }

        private BlockInfo MakeFirstInfo()
        {
            return new BlockInfo()
            {
                collectableIndex = 0,
                length = 3,
                looksRight = true
            };
        }
    }
}