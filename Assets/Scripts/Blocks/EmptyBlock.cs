using System;
using UnityEngine;

namespace Blocks
{
    public sealed class EmptyBlock : Block
    {
        public static event Action OnEmptyCollect;
        public override void Initialize(int index, int rewardCount)
        {
            _blockNumber.text = (index + 1).ToString();
        }

        protected override void ClaimReward(Collider other)
        {
            OnEmptyCollect?.Invoke();
        }
    }
}