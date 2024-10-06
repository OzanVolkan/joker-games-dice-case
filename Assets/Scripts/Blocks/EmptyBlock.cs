using UnityEngine;

namespace Blocks
{
    public sealed class EmptyBlock : Block
    {
        public override void Initialize(int index, int rewardCount)
        {
            _blockNumber.text = (index + 1).ToString();
        }
    }
}