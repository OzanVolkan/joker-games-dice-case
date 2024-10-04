using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Blocks
{
    public abstract class Block : MonoBehaviour
    {
        [Header("Block Visuals")] [SerializeField]
        protected TextMeshProUGUI _rewardCountText;

        [SerializeField] protected TextMeshProUGUI _blockNumber;

        private Sprite _blockSprite;

        public virtual void InitializeUI(int index, int rewardCount)
        {
            _rewardCountText.text = ($"x{rewardCount}");
            _blockNumber.text = (index + 1).ToString();
        }
    }
}