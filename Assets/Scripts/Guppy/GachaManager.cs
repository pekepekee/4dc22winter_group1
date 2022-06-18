using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Live2D.Cubism.Rendering;
using UnityEngine.Events;

public enum GachaState
{
    HOME,
    WAIT,
    START,
    SHOW_RESULT,
    FINISH
}

public class GachaManager : MonoBehaviour
{
    public GachaParams gachaParameter;
    public GachaParams tutorialGachaParameter;
    public GachaUIManager gachaUIManager;
    public GachaAnimation gachaResultAnimation;
    public GachaStartAnimation gachaStartAnimation;

    public float gachaAnimationTime = 1.0f;

    public bool canSkip = true;
    public bool debugMode = false;

    public GameObject LoadingGameObject;

    private Gacha gacha;
    private GachaState gachaState;

    private bool doSkip = false;
    private List<GachaItem> gachaResults;

    private float animationTime = 0f;
    private bool showKakuritsuUI = false;

    // Start is called before the first frame update
    void Start()
    {
        gacha = new Gacha();

        gachaState = GachaState.HOME;
        gachaResults = new List<GachaItem>();

        List<GachaWeight> gachaData = gachaParameter.gachaData;

        // チュートリアル専用のガチャ
        if (DataManager.IsTutorialMode())
        {
            gachaData = tutorialGachaParameter.gachaData;
        }

        foreach (GachaWeight weightData in gachaData)
        {
            gacha.RegisterItem(weightData.item, weightData.weight);
        }

        OnChangeState(gachaState);
    }

    // Update is called once per frame
    void Update()
    {
        GachaState newState = UpdateState(gachaState);
        if(gachaState != newState)
        {
            OnChangeState(newState);
            gachaState = newState;
        }
        UpdateInput(gachaState);

        animationTime = Mathf.Max(animationTime - Time.deltaTime, 0.0f);
    }

    /*
     * ステートを強制的に変更する
     */
    private void ChangeState(GachaState newState)
    {
        OnChangeState(newState);
        gachaState = newState;
    }

    /*
     * ガチャアニメを一括して更新する
     * アニメステートに変更があればその値を返す
     */
    private GachaState UpdateState(GachaState state)
    {
        GachaState newState = state;

        if(state == GachaState.START)
        {
            if(animationTime <= 0.0f)
            {
                newState = GachaState.SHOW_RESULT;
            }
            /*
            if (gachaVideoPlayer.isPlaying == false)
            {
                newState = GachaState.SHOW_RESULT;
            }
            */
        }
        else if(state == GachaState.SHOW_RESULT)
        {
            if (doSkip)
            {
                gachaResultAnimation.Skip();
                doSkip = false;
            }
            if (!gachaResultAnimation.IsAnimPlaying())
            {
                if (gachaResultAnimation.HasNext())
                {
                    gachaResultAnimation.Next();
                }
                else
                {
                    newState = GachaState.FINISH;
                }
            }
        }

        return newState;
    }

    private void OnChangeState(GachaState state)
    {
        UpdateUI(state);
        gachaStartAnimation.UpdateState(state);
        if(state == GachaState.FINISH)
        {
            gachaResultAnimation.FinishAnimation();
            gachaUIManager.ShowGachaResult(gachaResults);
        }
    }

    private void UpdateUI(GachaState state)
    {
        if (state != GachaState.HOME)
        {
            showKakuritsuUI = false;
        }

        if (GameClearManager.instance.IsGameClear())
        {
            gachaUIManager.UpdateUI(state, gachaParameter, canSkip: canSkip, showKakuritsuUI: showKakuritsuUI, isGameClear: true);
        }
        else if (DataManager.IsTutorialMode())
        {
            gachaUIManager.UpdateUI(state, gachaParameter, canSkip: canSkip, showKakuritsuUI: showKakuritsuUI, isTutorial: true);
        }
        else
        {
            gachaUIManager.UpdateUI(state, gachaParameter, canSkip: canSkip, showKakuritsuUI: showKakuritsuUI);
        }

        if(state == GachaState.START)
        {
            animationTime = gachaAnimationTime;
        }
    }

    private void UpdateInput(GachaState state)
    {
        if(state == GachaState.WAIT)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ChangeState(GachaState.START);
            }
        }
        if(state == GachaState.SHOW_RESULT)
        {
            if (Input.GetMouseButtonDown(0))
            {
                doSkip = true;
            }
        }
    }

    public void StartGacha(int count)
    {
        if(DataManager.IsEndlessMode() || debugMode || gachaParameter.CanPlay(DataManager.GetPoint()))
        {
            if (!DataManager.IsEndlessMode() && !debugMode)
            {
                DataManager.UsePoint(gachaParameter.requirePoint);
            }

            List<GachaItem> results = DoGacha(count);

            LoadingGameObject.SetActive(true);
            DelayAsync(UnityEngine.Random.Range(0.3f,1f), () =>
            {
                SetUpGachaResult(results);
                LoadingGameObject.SetActive(false);
                ChangeState(GachaState.WAIT);
            }).Forget();
            
        }
        else
        {
            Debug.Log("ポイントが足りません");
        }
    }

    public void SkipGacha()
    {
        gachaResultAnimation.SkipAll();
        ChangeState(GachaState.FINISH);
    }

    private void SetUpGachaResult(List<GachaItem> results)
    {
        gachaResults = results;

        foreach (GachaItem item in results)
        {
            if (item.isGameClearItem && !DataManager.IsEndlessMode())
            {
                GameClearManager.instance.SetGameClear(true);
            }
            DataManager.AddGachaItemResult(item);
            gachaResultAnimation.AddGachaItem(item);
        }
    }

    private List<GachaItem> DoGacha(int count)
    {
        List<GachaItem> gachaResults = new List<GachaItem>();

        for (int i = 0; i < count; i++)
        {
            DataManager.AddGachaCount(1);
            GachaItem result = gacha.GetResult();

            if(TenjoManager.instance.CheckTenjo(gachaParameter))
            {
                // ガチャ天井アイテム
                result = gachaParameter.tenjouItem;
            }
            gachaResults.Add(result);
        }

        return gachaResults;
    }

    public void OnClickBackButton()
    {
        if (DataManager.IsEndlessMode())
        {
            DataManager.EndEndlessMode();
            SceneManager.LoadScene("Title");
        }
        else if (GameClearManager.instance.IsGameClear())
        {
            if (DataManager.IsTutorialMode())
            {
                GameClearManager.instance.GameClearEvent(isExtraEnd: true);
            }
            else
            {
                GameClearManager.instance.GameClearEvent(isExtraEnd: false);
            }
        }
        else if (DataManager.IsTutorialMode())
        {
            SceneManager.LoadScene("sanmaif");
        }
        else
        {
            BackToMenu();
        }
    }

    public void ShowKakuritsuUI()
    {
        showKakuritsuUI = true;
        UpdateUI(gachaState);
    }

    public void HideKakuritsuUI()
    {
        showKakuritsuUI = false;
        UpdateUI(gachaState);
    }

    void BackToMenu()
    {
        // メニューに戻る処理
        SceneManager.LoadScene("Home");
    }
    
    private static async UniTask DelayAsync(float seconds, UnityAction callback)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
        callback?.Invoke();
    }
}
