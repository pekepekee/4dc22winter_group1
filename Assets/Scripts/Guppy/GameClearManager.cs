using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    public static GameClearManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private bool gameClear;

    public void SetGameClear(bool state)
    {
        gameClear = true;
    }

    public bool IsGameClear()
    {
        return gameClear;
    }

    public void GameClearEvent(bool isExtraEnd = false)
    {
        if (isExtraEnd)
        {
            Debug.Log("ExtraEND!!!");
            // ���G���h�Ɉړ�
            SceneManager.LoadScene("uraend");
        }
        else
        {
            Debug.Log("NormalEND!!!");
            // �ʏ�G���h�Ɉړ�
            SceneManager.LoadScene("ending");
        }
    }
}
