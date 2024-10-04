using UnityEngine;

namespace Blocks
{
    public sealed class EmptyBlock : Block
    {
        public override void InitializeUI(int index, int rewardCount)
        {
            _blockNumber.text = (index + 1).ToString();
        }
    }
}