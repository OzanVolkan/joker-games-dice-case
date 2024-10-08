using TMPro;
using UnityEngine;

namespace Map.Blocks
{
    public abstract class Block : MonoBehaviour
    {
        [Header("Block Visuals")] 
        [SerializeField] protected TextMeshProUGUI _rewardCountText;
        [SerializeField] protected TextMeshProUGUI _blockNumber;

        [Header("Block Particle Effect")]
        [SerializeField] protected ParticleSystem _blockParticleEffect;
        
        protected int _rewardCount;

        public virtual void Initialize(int index, int rewardCount)
        {
            _rewardCountText.text = ($"x{rewardCount}");
            _blockNumber.text = (index + 1).ToString();
            _rewardCount = rewardCount;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            ClaimReward(other);
            _blockParticleEffect.Play();
        }

        protected abstract void ClaimReward(Collider other);
    }
}