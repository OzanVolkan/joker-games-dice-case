using System;
using UnityEngine;

namespace Blocks
{
    public sealed class StrawberryBlock : Block
    {
        public static event Action<int, Vector3> OnStrawberryCollect;
        
        protected override void ClaimReward(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnStrawberryCollect?.Invoke(_rewardCount, transform.position);
            }
        }
    }
}
