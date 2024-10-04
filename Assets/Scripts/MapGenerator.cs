using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private Transform _parentTransform;

    private readonly float _blockOffsetMultiplier = 8.15f;
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
            var pos = new Vector3(0f, 0, element.index * _blockOffsetMultiplier);
            var block = Instantiate(_blockPrefab, pos, Quaternion.identity, _parentTransform);

            if (element.type != "empty")
            {
                // Blok üzerindeki objenin adını ve miktarını göster
                //block.GetComponent<Block>().Initialize(element.type, element.count);
            }
        }
    }
}