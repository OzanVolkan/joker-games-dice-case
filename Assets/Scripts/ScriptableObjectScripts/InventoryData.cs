using UnityEngine;

namespace ScriptableObjectScripts
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "Data/InventoryData", order = 0)]
    public class InventoryData : ScriptableObject
    {
        [SerializeField] private int _appleCount;
        [SerializeField] private int _pearCount;
        [SerializeField] private int _strawberryCount;

        #region Properties

        public int AppleCount
        {
            get => _appleCount;
            set => _appleCount = value;
        } 
        public int PearCount
        {
            get => _pearCount;
            set => _pearCount = value;
        }  
        public int StrawberryCount
        {
            get => _strawberryCount;
            set => _strawberryCount = value;
        }

        #endregion
    }
}