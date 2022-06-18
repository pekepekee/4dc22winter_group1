using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaButtonEvent : MonoBehaviour
{
    public GachaManager gachaManager;

    public void StartGacha(int count)
    {
        gachaManager.StartGacha(count);
    }

    public void SkipGacha()
    {
        gachaManager.SkipGacha();
    }

    public void BackToMenu()
    {
        gachaManager.OnClickBackButton();
    }

    public void ShowKakuritsu()
    {
        gachaManager.ShowKakuritsuUI();
    }

    public void HideKakuritsu()
    {
        gachaManager.HideKakuritsuUI();
    }
}
