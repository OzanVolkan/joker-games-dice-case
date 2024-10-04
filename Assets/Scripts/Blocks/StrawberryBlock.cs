namespace Blocks
{
    public sealed class StrawberryBlock : Block
    {
        public StrawberryBlock(int index, string type, int rewardCount) : base(index, type, rewardCount)
        {
            InitializeUI(index,rewardCount);
        }
    }
}
