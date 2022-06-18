using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GachaWeight
{
    public GachaItem item;
    public int weight;
}

[CreateAssetMenu(fileName = "GachaParameter", menuName = "Guppy/GachaParameter", order = 1100)]
public class GachaParams : ScriptableObject
{
    public int requirePoint = 1000;
    public int tenjou = 50;
    public GachaItem tenjouItem;
    public List<GachaWeight> gachaData;

    public string GachaButtonText_Home()
    {
        return $"10連ガチャ({requirePoint}P)";
    }

    public string GachaButtonText_Retry()
    {
        return $"もう一度({requirePoint}P)";
    }

    public bool CanPlay(int currentPoint)
    {
        return currentPoint >= requirePoint;
    }
}
