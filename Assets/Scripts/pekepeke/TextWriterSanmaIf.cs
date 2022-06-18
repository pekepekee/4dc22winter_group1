using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextWriterSanmaIf : MonoBehaviour
{
    public Uitext uitext;
    public ImageChange change;
    public LoadingScreenManager load;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Cotest");
    }

    IEnumerator Skip()
    {
        while (uitext.playing) yield return 0;
        while (!uitext.IsClicked()) yield return 0;
    }

    IEnumerator Home()
    {
        if (Input.GetMouseButtonDown(0))
        {
            load.LoadLoadingScreen();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Home");
        }
        yield return 0;
    }
    // 文章を表示させるコルーチン
    IEnumerator Cotest()
    {
        uitext.DrawText("依頼主", "サンマしか当たらなかったですね・・・");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵", "まあまだ10回しか引いてないからねぇ。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "そうですね。気長にガチャを回し続けますか。");
        yield return StartCoroutine("Skip");

        change.ChangeSmile();
        uitext.DrawText("探偵", "ええ。そうしましょう。");
        yield return StartCoroutine("Skip");

        yield return StartCoroutine("Home");
    }

}
