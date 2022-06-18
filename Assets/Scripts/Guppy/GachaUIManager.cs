using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Live2D.Cubism.Rendering;

public class GachaUIManager : MonoBehaviour
{
    public GameObject gachaHomeUIObject;
    public GameObject gachaWaitUIObject;
    public GameObject gachaStartUIObject;
    public GameObject gachaShowResultUIObject;
    public GameObject gachaFinishUIObject;

    public GameObject skipButton;
    public GameObject retryButton;
    public GameObject backButtonHome;
    public GameObject backButtonFinish;

    // public Text startGachaButtonText;
    // public Text retryGachaButtonText;
    public Text tenjoTextHome;
    public Text tenjoTextFinish;
    public AudioSource gachaSE;
    public AudioSource gachaBGM;
    public GachaResultUI resultItemUI;
    public GameObject pointErrorHome;
    public GameObject pointErrorFinish;
    public GameObject kakuritsuUI;

    public void UpdateUI(
        GachaState state,
        GachaParams gachaParameter,
        bool canSkip = true,
        bool isTutorial = false,
        bool isGameClear = false,
        bool showKakuritsuUI = false
        )
    {
        gachaHomeUIObject.SetActive(false);
        gachaWaitUIObject.SetActive(false);
        gachaStartUIObject.SetActive(false);
        gachaShowResultUIObject.SetActive(false);
        gachaFinishUIObject.SetActive(false);
        kakuritsuUI.SetActive(showKakuritsuUI);

        if (state == GachaState.HOME)
        {
            gachaHomeUIObject.SetActive(true);

            // startGachaButtonText.text = gachaParameter.GachaButtonText_Home();

            if(gachaBGM.isPlaying == false)
            {
                gachaBGM.Play();
            }
            backButtonHome.SetActive(true);
            if (isTutorial)
            {
                backButtonHome.SetActive(false);
            }

            if (gachaParameter.CanPlay(DataManager.GetPoint()) || DataManager.IsEndlessMode())
            {
                pointErrorHome.SetActive(false);
            }
            else
            {
                pointErrorHome.SetActive(true);
            }

            TenjoManager.instance.UpdateUI(gachaParameter, tenjoTextHome);
        }
        else if (state == GachaState.WAIT)
        {
            gachaWaitUIObject.SetActive(true);
            gachaBGM.Stop();
        }
        else if (state == GachaState.START)
        {
            gachaStartUIObject.SetActive(true);
            gachaBGM.Stop();

            gachaSE.Play();
        }
        else if (state == GachaState.SHOW_RESULT)
        {
            gachaShowResultUIObject.SetActive(true);
            gachaBGM.Stop();

            skipButton.SetActive(true);

            if (!canSkip)
            {
                skipButton.SetActive(false);
            }
        }
        else if (state == GachaState.FINISH)
        {
            gachaFinishUIObject.SetActive(true);

            gachaBGM.Play();

            // startGachaButtonText.text = gachaParameter.GachaButtonText_Retry();

            retryButton.SetActive(true);
            backButtonFinish.SetActive(true);

            TenjoManager.instance.UpdateUI(gachaParameter, tenjoTextFinish);

            if (gachaParameter.CanPlay(DataManager.GetPoint()) || DataManager.IsEndlessMode())
            {
                pointErrorFinish.SetActive(false);
            }
            else
            {
                pointErrorFinish.SetActive(true);
            }

            // クリア時またはチュートリアル時にはリトライできない
            if (isGameClear || isTutorial)
            {
                retryButton.SetActive(false);
                pointErrorFinish.SetActive(false);
            }
        }
    }

    public void ShowGachaResult(List<GachaItem> items)
    {
        resultItemUI.Clear();

        foreach (GachaItem item in items)
        {
            resultItemUI.AddResult(item);
        }
    }
}
