using System;
using UnityEngine;

namespace Blocks
{
    public sealed class AppleBlock : Block
    {
        public static event Action<int, Vector3> OnAppleCollect;
        
        protected override void ClaimReward(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnAppleCollect?.Invoke(_rewardCount, transform.position);
            }
        }

    }
}