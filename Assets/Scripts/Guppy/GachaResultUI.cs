using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaResultUI : MonoBehaviour
{
    public GameObject UIItem;
    public Transform UITransform;
    private List<GameObject> UIItemList;

    void Start()
    {
        UIItemList = new List<GameObject>();
    }

    public void Clear()
    {
        if(UIItemList.Count > 0)
        {
            foreach (GameObject UIItem in UIItemList)
            {
                Destroy(UIItem);
            }
        }

        UIItemList.Clear();
    }

    public void AddResult(GachaItem item)
    {
        GameObject instance = Instantiate(UIItem, UITransform);
        UIItemList.Add(instance);

        instance.GetComponentInChildren<Image>().sprite = item.itemImage;
    }
}
