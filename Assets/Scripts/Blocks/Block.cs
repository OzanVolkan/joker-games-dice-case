using System;
using TMPro;
using UnityEngine;

namespace Blocks
{
    public abstract class Block : MonoBehaviour
    {
        [Header("Block Visuals")] [SerializeField]
        protected TextMeshProUGUI _rewardCountText;

        [SerializeField] protected TextMeshProUGUI _blockNumber;

        protected int _rewardCount;

        public virtual void Initialize(int index, int rewardCount)
        {
            _rewardCountText.text = ($"x{rewardCount}");
            _blockNumber.text = (index + 1).ToString();
            _rewardCount = rewardCount;
        }

        private void OnTriggerEnter(Collider other)
        {
            ClaimReward(other);
        }

        protected abstract void ClaimReward(Collider other);
    }
}