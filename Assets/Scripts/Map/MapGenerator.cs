using System.Collections.Generic;
using System.IO;
using Blocks;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform _parentTransform;

    private readonly float _blockOffsetMultiplier = 8.15f;
    private readonly string _mapFileName = "map";

    private void Start()
    {
        var mapData = LoadMapData(_mapFileName);
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
        var blockCount = 0;
        
        foreach (var element in mapData.map)
        {
            var pos = new Vector3(0f, 0, element.index * _blockOffsetMultiplier);
            
            BlockFactory.Instance.GetProduct(element.type, pos, _parentTransform, element.index, element.count);
            
            blockCount++;
        }

        PlayerController.Instance.BlockCount = blockCount;
    }
}