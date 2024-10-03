using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private Transform _parentTransform;

    private readonly string _mapFileName = "map";

    private void Start()
    {
        var mapData = LoadMapData(_mapFileName);
        Debug.Log(JsonUtility.ToJson(mapData, true));
        CreateMap(mapData);
    }

    private MapData LoadMapData(string fileName)
    {
        var jsonFile = Resources.Load<TextAsset>(fileName);
        var mapData = JsonUtility.FromJson<MapData>(jsonFile.text);
        return mapData;
    }

    private void CreateMap(MapData mapData)
    {
        foreach (var element in mapData.map)
        {
            var block = Instantiate(_blockPrefab, new Vector3(0, 0, element.index), Quaternion.identity);

            if (element.type != "empty")
            {
                // Blok üzerindeki objenin adını ve miktarını göster
                //block.GetComponent<Block>().Initialize(element.type, element.count);
            }
        }
    }
}