using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessModeInit : MonoBehaviour
{
    public bool isEndlessMode = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (isEndlessMode)
        {
            DataManager.ResetGachaItemResult();
            DataManager.SetGachaCount(0);
            DataManager.StartEndlessMode();
        }
    }
}
