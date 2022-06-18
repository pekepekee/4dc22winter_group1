using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    private const int initialPoint = 10000;
    // Start is called before the first frame update
    void Start()
    {
        DataManager.ResetGachaItemResult();
        DataManager.SetGachaCount(0);
        DataManager.SetPoint(initialPoint);
        DataManager.SetTutorialMode(true);

        Debug.Log("Initialized Game");
    }
}
