using System;
using TMPro;
using UnityEngine;

namespace Blocks
{
    public abstract class Block : MonoBehaviour
    {
        public static event Action<int, Vector3> OnClaimReward;

        [Header("Block Visuals")] [SerializeField]
        protected TextMeshProUGUI _rewardCountText;

        [SerializeField] protected TextMeshProUGUI _blockNumber;

        private int _rewardCount;
        private Sprite _blockSprite;

        public virtual void Initialize(int index, int rewardCount)
        {
            _rewardCountText.text = ($"x{rewardCount}");
            _blockNumber.text = (index + 1).ToString();
            _rewardCount = rewardCount;
        }

        private void OnTriggerEnter(Collider other)
        {
            print("Triggera Girdi");
            ClaimReward(other);
        }

        protected void ClaimReward(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                print("Player girdi");
                OnClaimReward?.Invoke(_rewardCount, transform.position);
            }
        }
    }
}