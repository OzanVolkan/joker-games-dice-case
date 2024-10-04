namespace Blocks
{
    public sealed class PearBlock : Block
    {
        public PearBlock(int index, string type, int rewardCount) : base(index, type, rewardCount)
        {
            InitializeUI(index,rewardCount);
        }
    }
}
