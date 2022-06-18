using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextWriterTuterial : MonoBehaviour
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

    IEnumerator TutorialGacha()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("チュートリアルガチャへ");
            load.LoadLoadingScreen();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("PlayGachaAndResult");
        }
        yield return 0;
    }


    // 文章を表示させるコルーチン
    IEnumerator Cotest()
    {
        uitext.DrawText("プロローグ");
        yield return StartCoroutine("Skip");

        uitext.DrawText("夏の暑さが和らぎ、木の葉の青々とした山はだんたんと色鮮やかになった。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("その山を背景に、人が老人と子供しかおらず若い者は出稼ぎに行く貧しい田舎村があった。");
        yield return StartCoroutine("Skip");


        uitext.DrawText("依頼主", "実はですねぇ、今年の夏頃に村の北の方に新しく魚屋ができたんですが・・・");
        yield return StartCoroutine("Skip");

        change.ChangeMagao();
        uitext.DrawText("探偵", "・・・");
        yield return StartCoroutine("Skip");

        uitext.DrawText("村の集会所の隅で依頼主と探偵は、机を挟み淹れたての緑茶を飲みながら会話をしている。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "村人の話しによると、その店では新鮮な海の魚を売っているとのことなんですが、どうにも抽選機を回して出た色で買える魚が変わるという話で。");
        yield return StartCoroutine("Skip");

        change.ChangeSmile();
        uitext.DrawText("探偵", "ほうほう、それで");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "はい、それで私のほうでその店に聞くと、店員曰く「ガチャ？」という方式の売り方をしているみたいなんです。");
        yield return StartCoroutine("Skip");

        change.ChangeSad();
        uitext.DrawText("探偵", "確かに変な売り方ですね。とても儲けが出る売り方とは思えないです。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "私もそう思うのですが、何回抽選してもマグロが出てきたところは見たことがないと村人から陳情が来ていて、本当にマグロを売っているか怪しいのです。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "そこで探偵さんには、本当にマグロを仕入れているのかそのお店を調査してもらいたくて・・・");
        yield return StartCoroutine("Skip");

        change.ChangeMagao();
        uitext.DrawText("探偵", "・・・");
        yield return StartCoroutine("Skip");

        uitext.DrawText("集会所の隅に一瞬の静寂が訪れる。探偵は少し考える素振りをし、口を開いた。");
        yield return StartCoroutine("Skip");

        change.ChangeSmile();
        uitext.DrawText("探偵", "分かりました。しかし、かなりの費用はかかると思います。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("この村は海からかなり離れており、新鮮な魚を運んでくるには相応の費用が掛かることは想像がつく。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("しかし、話題に挙がった魚屋で本当にマグロ仕入れているのかは分からない。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵はどことなく深い闇を感じた。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主と探偵は、かき集めた軍資金を頼りに怪しい魚屋の闇を暴くことにした。");
        yield return StartCoroutine("Skip");
        yield return StartCoroutine("TutorialGacha");
    }
    
}
