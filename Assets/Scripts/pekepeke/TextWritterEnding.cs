using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextWritterEnding : MonoBehaviour
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

    IEnumerator Title()
    {
        if (Input.GetMouseButtonDown(0))
        {
            load.LoadLoadingScreen();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Title");
        }
        yield return 0;
    }

    // 文章を表示させるコルーチン
    IEnumerator Cotest()
    {
        uitext.DrawText("エンディング");
        yield return StartCoroutine("Skip");

        uitext.DrawText("魚屋の前に項垂れる二人の姿があった。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "サンマしか出ないではないですか。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵", "天井して初めてマグロが出てくるなんて・・・");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "これ絶対確率いじってますよね。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵", "そうだね。100パーいじってるね。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "この魚屋噂通りほんとに闇深いですね");
        yield return StartCoroutine("Skip");

        change.ChangeAngry();
        uitext.DrawText("探偵", "もう金むしりとる気しかないからな！");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "ほんとその通りですよね");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵", "汚い大人やな！汚い大人やで！ホンマ！");
        yield return StartCoroutine("Skip");
        yield return StartCoroutine("Title");
    }
}
