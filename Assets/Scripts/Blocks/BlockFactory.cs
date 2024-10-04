using System;
using Interfaces;
using UnityEngine;

namespace Blocks
{
    public class BlockFactory : MonoBehaviour, IFactory
    {
        public static BlockFactory Instance;

        [Header("Block Prefabs")] [SerializeField]
        private GameObject _appleBlock;

        [SerializeField] private GameObject _pearBlock;
        [SerializeField] private GameObject _strawberryBlock;
        [SerializeField] private GameObject _epmtyBlock;

        #region Consts

        public const string BLOCK_TYPE_APPLE = "apple";
        public const string BLOCK_TYPE_PEAR = "pear";
        public const string BLOCK_TYPE_STRAWBERRY = "strawberry";
        public const string BLOCK_TYPE_EMPTY = "empty";

        #endregion

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }


        public IProduct GetProduct(string productType, Vector3 pos, Transform parent, int index, int rewardCount)
        {
            switch (productType)
            {
                case "apple":
                    var appleBlock = Instantiate(_appleBlock, pos, Quaternion.identity, parent);
                    return new AppleBlock(index, productType, rewardCount);
                case "pear":
                    var pearBlock = Instantiate(_appleBlock, pos, Quaternion.identity, parent);
                    return new PearBlock(index, productType, rewardCount);
                case "strawberry":
                    var strawberryBlock = Instantiate(_appleBlock, pos, Quaternion.identity, parent);
                    return new StrawberryBlock(index, productType, rewardCount);
                case "empty":
                    var emptyBlock = Instantiate(_appleBlock, pos, Quaternion.identity, parent);
                    return new EmptyBlock(index, productType);
                default:
                    throw new Exception("Invalid product type!");
            }
        }
    }
}