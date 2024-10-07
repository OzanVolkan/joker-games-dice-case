using UnityEngine;
using System;

namespace Blocks
{
    public sealed class PearBlock : Block
    {
        public static event Action<int, Vector3> OnPearCollect;

        protected override void ClaimReward(Collider other)
        {
            OnPearCollect?.Invoke(_rewardCount, transform.position);
        }
    }
}