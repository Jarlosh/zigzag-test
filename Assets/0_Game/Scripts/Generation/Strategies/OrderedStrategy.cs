using UnityEngine;

namespace _0_Game.Scripts.Generation
{
    public class OrderedStrategy : BaseStrategy
    {
        public OrderedStrategy(int min, int max) : base(min, max) { }
        
        protected override BlockInfo MakeFirstInfo()
        {
            return new BlockInfo(PickLength(), 0, true);
        }

        protected override BlockInfo MakeNextBlockInfo()
        {
            var len = PickLength();
            var colIndex = (lastInfo.collectableIndex + 1) % len;
            return new BlockInfo(len, colIndex, !lastInfo.looksRight);
        }
    }
}