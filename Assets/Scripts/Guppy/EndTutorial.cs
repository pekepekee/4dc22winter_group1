using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial : MonoBehaviour
{
    void Start()
    {
        DataManager.SetTutorialMode(false);
        Debug.Log("Tutorial END");
    }
}
