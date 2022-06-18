using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextWriterUraEnd : MonoBehaviour
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
        uitext.DrawText("裏エンディング");
        yield return StartCoroutine("Skip");

        uitext.DrawText("辺りには何とも言えない空気が漂っていた。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "うーん・・・あっさりマグロ出ちゃいましたねぇ");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵", "まぁ健全な商売をしていてよかったのではないですか?");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "そうですね。この魚屋が悪事を働いていなくてよかったです。");
        yield return StartCoroutine("Skip");

        change.ChangeSmile();
        uitext.DrawText("探偵", "それでは依頼料なんですけども・・・");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "はい。いくらになりますか？");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵", "15万円になります。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "はい？さすがに高すぎやしませんか?");
        yield return StartCoroutine("Skip");

        change.ChangeAngry();
        uitext.DrawText("探偵", "いえ依頼料は依頼料なんで払ってもらわないと困ります。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "あんた金むしり取る気しかないやんか。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "汚い大人やな！汚い大人やで！ホンマ！");
        yield return StartCoroutine("Skip");


        yield return StartCoroutine("Title");
    }
}
