using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject LoadingOverlay;

    public void LoadLoadingScreen()
    {
        LoadingOverlay.SetActive(true);
    }
    
}
