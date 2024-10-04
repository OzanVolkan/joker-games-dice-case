using System;
using UnityEngine;

[Serializable]
public class MapElement
{
    public int index;
    public string type;
    public int count;

    public MapElement(int index, string type, int count = 0)
    {
        this.index = index;
        this.type = type;
        this.count = count;
    }
}