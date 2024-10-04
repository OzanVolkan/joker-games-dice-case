namespace Blocks
{
    public sealed class AppleBlock : Block
    {
        public AppleBlock(int index, string type, int rewardCount) : base(index, type, rewardCount)
        {
            InitializeUI(index, rewardCount);
        }
    }
}