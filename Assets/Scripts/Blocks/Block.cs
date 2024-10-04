using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Blocks
{
    public abstract class Block : MonoBehaviour
    {
        [Header("Block Visuals")]
        [SerializeField] protected Image _blockImage;
        [SerializeField] protected TextMeshProUGUI _rewardCountText;
        [SerializeField] protected TextMeshProUGUI _blockNumber;

        private Sprite _blockSprite;
    
        protected Block(int index, string type, int rewardCount)
        {
            _blockSprite = Resources.Load<Sprite>($"Sprites/{type}");
        }
    
        protected virtual void InitializeUI(int index, int rewardCount)
        {
            _blockImage.sprite = _blockSprite;
            _rewardCountText.text = rewardCount.ToString();
            _blockNumber.text = (index + 1).ToString();
        }

    }
}
