using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGachaResult : MonoBehaviour
{
    public GachaItem magroItem;
    public GachaItem sanmaItem;
    public Text magroText;
    public Text sanmaText;

    void Update()
    {
        magroText.text = $"x{DataManager.GetGachaItemResultCount(magroItem)}";
        sanmaText.text = $"x{DataManager.GetGachaItemResultCount(sanmaItem)}";
    }
}
