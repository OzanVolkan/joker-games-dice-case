using System;
using UnityEngine;

namespace Map.Blocks
{
    public sealed class AppleBlock : Block
    {
        public static event Action<int, Vector3> OnAppleCollect;
        
        protected override void ClaimReward(Collider other)
        {
            OnAppleCollect?.Invoke(_rewardCount, transform.position);
        }

    }
}