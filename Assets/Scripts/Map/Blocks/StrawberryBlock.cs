using System;
using UnityEngine;

namespace Map.Blocks
{
    public sealed class StrawberryBlock : Block
    {
        public static event Action<int, Vector3> OnStrawberryCollect;
        
        protected override void ClaimReward(Collider other)
        {
            OnStrawberryCollect?.Invoke(_rewardCount, transform.position);
        }
    }
}
