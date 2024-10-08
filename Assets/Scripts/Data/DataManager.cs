using System.IO;
using Managers;
using ScriptableObjectScripts;
using UnityEngine;

namespace Data
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private InventoryData _inventoryData;
        private string _filePath;
        
        private void Start()
        {
            _filePath = Path.Combine(Application.persistentDataPath, "gamedata.json");
            LoadData();
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
                SaveData();
        }

        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                var jsonData = File.ReadAllText(_filePath);
                JsonUtility.FromJsonOverwrite(jsonData, _inventoryData);
            }
            else
            {
                Debug.Log("Data file not found. Using default inventory values");
            }

            InventoryManager.Instance.AppleCount = _inventoryData.AppleCount;
            InventoryManager.Instance.PearCount = _inventoryData.PearCount;
            InventoryManager.Instance.StrawberryCount = _inventoryData.StrawberryCount;
        }

        private void SaveData()
        {
            _inventoryData.AppleCount = InventoryManager.Instance.AppleCount;
            _inventoryData.PearCount = InventoryManager.Instance.PearCount;
            _inventoryData.StrawberryCount = InventoryManager.Instance.StrawberryCount;
            
            var jsonData = JsonUtility.ToJson(_inventoryData, true);
            File.WriteAllText(_filePath, jsonData);
        }
    }
}