using System;
using Interfaces;
using UnityEngine;

namespace Map.Blocks
{
    public class BlockFactory : MonoBehaviour, IFactory
    {
        public static BlockFactory Instance;

        [Header("Block Prefabs")] [SerializeField]
        private GameObject _appleBlock;

        [SerializeField] private GameObject _pearBlock;
        [SerializeField] private GameObject _strawberryBlock;
        [SerializeField] private GameObject _epmtyBlock;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }


        public void GetProduct(string productType, Vector3 pos, Transform parent, int index, int rewardCount = 0)
        {
            switch (productType)
            {
                case "apple":
                    var appleBlock = Instantiate(_appleBlock, pos, Quaternion.identity, parent);
                    var appleBlockComp = appleBlock.GetComponent<AppleBlock>();
                    appleBlockComp.Initialize(index,rewardCount);
                    break;
                case "pear":
                    var pearBlock = Instantiate(_pearBlock, pos, Quaternion.identity, parent);
                    var pearBlockComp = pearBlock.GetComponent<PearBlock>();
                    pearBlockComp.Initialize(index,rewardCount);
                    break;

                case "strawberry":
                    var strawberryBlock = Instantiate(_strawberryBlock, pos, Quaternion.identity, parent);
                    var strawberryBlockComp = strawberryBlock.GetComponent<StrawberryBlock>();
                    strawberryBlockComp.Initialize(index, rewardCount);
                    break;

                case "empty":
                    var emptyBlock = Instantiate(_epmtyBlock, pos, Quaternion.identity, parent);
                    var emptyBlockComp = emptyBlock.GetComponent<EmptyBlock>();
                    emptyBlockComp.Initialize(index, 0);
                    break;

                default:
                    throw new Exception("Invalid product type!");
            }
        }
    }
}