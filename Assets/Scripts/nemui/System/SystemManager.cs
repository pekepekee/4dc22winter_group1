using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    private static SystemManager instance;
 
    // シングルトンにする
    public static SystemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (SystemManager)FindObjectOfType(typeof(SystemManager));
 
                if (instance == null)
                {
                    Debug.LogError(typeof(SystemManager) + "をアタッチしているGameObjectはありません");
                }
            }
 
            return instance;
        }
    }

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        OnClickEscapeKey();
    }

    private void OnClickEscapeKey()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("アプリが終了しました");
            Application.Quit();
        }else{
            return;
        }
    }
}
