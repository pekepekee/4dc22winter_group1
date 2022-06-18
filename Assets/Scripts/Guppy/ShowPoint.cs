using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPoint : MonoBehaviour
{
    public Text pointText;

    void Update()
    {
        if (DataManager.IsEndlessMode())
        {
            pointText.text = "∞円";
        }
        else
        {
            pointText.text = $"{DataManager.GetPoint()}円";
        }
    }
}
