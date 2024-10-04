namespace Blocks
{
    public sealed class EmptyBlock : Block
    {
        private const int DefaultRewardCount = 0;

        public EmptyBlock(int index, string type) : base(index, type, DefaultRewardCount)
        {
            InitializeUI(index, DefaultRewardCount);
        }

        protected override void InitializeUI(int index, int rewardCount)
        {
            _blockImage.sprite = null;
            _rewardCountText.text = "";
            _blockNumber.text = (index + 1).ToString();
        }
    }
}