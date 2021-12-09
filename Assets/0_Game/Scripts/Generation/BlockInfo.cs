namespace _0_Game.Scripts.Generation
{
    public struct BlockInfo
    {
        public int length;
        public bool looksRight;
        public int collectableIndex;

        public BlockInfo(int length, int collectableIndex, bool looksRight)
        {
            this.length = length;
            this.collectableIndex = collectableIndex;
            this.looksRight = looksRight;
        }
    }
}