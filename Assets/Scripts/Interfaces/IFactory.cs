using UnityEngine;

namespace Interfaces
{
    public interface IFactory
    {
        public IProduct GetProduct(string productType, Vector3 pos, Transform parent, int index, int rewardCount);
    }
}
