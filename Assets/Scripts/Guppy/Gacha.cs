using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha
{
    private List<GachaItem> gachaItems;
    private Dictionary<GachaItem, int> gachaWeights;
    private int gachaAllWeight = 0;

    public Gacha()
    {
        gachaItems = new List<GachaItem>();
        gachaWeights = new Dictionary<GachaItem, int>();
    }

    public void RegisterItem(GachaItem item, int weight)
    {
        gachaItems.Add(item);
        gachaWeights[item] = weight;
        gachaAllWeight += weight;
    }

    public void ClearItem(GachaItem item)
    {
        gachaItems.Remove(item);
        gachaWeights.Remove(item);
    }

    public GachaItem GetResult()
    {
        int randomResult = Random.Range(0, gachaAllWeight);

        foreach (GachaItem item in gachaItems)
        {
            if (randomResult < gachaWeights[item])
            {
                return item;
            }
            else
            {
                randomResult -= gachaWeights[item];
            }
        }
        return null;
    }
}
