using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageChange : MonoBehaviour
{

    public GameObject sad;
    public GameObject smile;
    public GameObject magao;
    public GameObject angry;

    public void ChangeSad()
    {
        sad.SetActive(true);
        smile.SetActive(false);
        magao.SetActive(false);
        angry.SetActive(false);
    }

    public void ChangeSmile()
    {
        sad.SetActive(false);
        smile.SetActive(true);
        magao.SetActive(false);
        angry.SetActive(false);
    }

    public void ChangeMagao()
    {
        sad.SetActive(false);
        smile.SetActive(false);
        magao.SetActive(true);
        angry.SetActive(false);
    }

    public void ChangeAngry()
    {
        sad.SetActive(false);
        smile.SetActive(false);
        magao.SetActive(false);
        angry.SetActive(true);
    }

}
