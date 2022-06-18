using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UniRx;
using _32ba;

public class QuizManager : MonoBehaviour
{
    public TextAsset questionFile;
    public GameObject[] answerButtonObjects;
    public GameObject timeLimitBarObject;
    public GameObject correctTextObject;
    public GameObject incorrectTextObject;
    public GameObject afterAnsweringPanelObject;
    public GameObject loadingOverlayObject;
    public AudioClip[] quizAudioClips;
    public AudioSource quizAudioSource;
    public Button nextButton;
    public Button homeButton;
    public CanvasGroup quizUiCanvasGroup;
    public Image timeBarImage;
    public Text questionText;

    private readonly List<string[]> _questionData = new List<string[]>();
    private int _questionId;
    private bool _isAlreadyAnswered = false;
    private bool _isEnableTimer = false;
    private float _countTime = 0;
    private float _progress = 0;
    private Button[] _answerButtons;

    private const int correctPointAward = 5000;

    // Start is called before the first frame update
    private void Start()
    {
        loadingOverlayObject.SetActive(true);
        Array.Resize(ref _answerButtons, answerButtonObjects.Length);
        for (int i = 0; i < answerButtonObjects.Length; i++) _answerButtons[i] = answerButtonObjects[i].GetComponent<Button>();
        bool isQuestionCsvLoaded = CsvReader.Read(questionFile, _questionData, '	'); //テキストファイルから問題をリストへ読み込み
        Debug.Log(isQuestionCsvLoaded ? "問題CSVの読み込みに成功しました" : "問題CSVの読み込みに失敗しました");
        _questionId = UnityEngine.Random.Range(0, _questionData.Count); //何問目を出題するかを決める
        DelayAsync(UnityEngine.Random.Range(0,0.8f), () =>
        {
            loadingOverlayObject.SetActive(false);
            SetQuestion(_questionData, _questionId); //問題文、回答を各テキストフィールドへ反映
            AudioPlayer(0);
            DelayAsync(1.2f, () =>
                    {
                        foreach (var t in answerButtonObjects) t.SetActive(true);
                        timeLimitBarObject.SetActive(true); //回答ボタンとタイムリミットを表示するバーを表示
                        _isEnableTimer = true; //タイマー有効化
                        AudioPlayer(1);
                    }).Forget();
        }).Forget();
        nextButton.OnClickAsObservable().First().Subscribe(_ => OnClickUINextButton()).AddTo(this);
        homeButton.OnClickAsObservable().First().Subscribe(_ => OnClickUIHomeButton().Forget()).AddTo(this);
        for (int i = 0; i < answerButtonObjects.Length; i++)
        {
            var num = i;
            _answerButtons[i].OnClickAsObservable().First().Subscribe(_ => OnClickAnswerButton(num)).AddTo(this);
        }
    }

    private void Update()
    {
        if (_isEnableTimer) TimeLimitCounter(5.0f); //タイマーが有効な間、タイマーを実行
    }

    /// <summary>
    /// 回答ボタンを押した時に呼ばれる関数
    /// </summary>
    /// <param name="buttonID">押されたボタンに対応するID</param>
    private void OnClickAnswerButton(int buttonID)
    {
        if (_isAlreadyAnswered) return;
        AudioPlayer(1,false);//すでに回答済みなら、その後はボタンを反応させないようにする
        _isAlreadyAnswered = true;
        _isEnableTimer = false;
        DelayAsync(1.0f, () => { afterAnsweringPanelObject.SetActive(true); }).Forget(); //次へ進むボタンを表示
        if (AnswerQuestion(_questionData, _questionId, buttonID))
        {
            //正解なら正しい答えをハイライトし、丸の記号を出し、ポイントを加算
            correctTextObject.SetActive(true);
            HighlightCorrectAnswer(_questionData, _questionId);
            DataManager.AddPoint(correctPointAward);
            AudioPlayer(2);
        }
        else
        {
            //不正解なら正しい答えをハイライトし、バツマークを出す
            incorrectTextObject.SetActive(true);
            HighlightCorrectAnswer(_questionData, _questionId);
            AudioPlayer(3);
        }
    }
    
    /// <summary>
    /// ホームへ戻るボタンが押されたときに呼ばれるクラス
    /// </summary>
    private async UniTaskVoid OnClickUIHomeButton()
    {
        loadingOverlayObject.SetActive(true);
        await DelayAsync(UnityEngine.Random.Range(0,0.5f), () =>
        {
            loadingOverlayObject.SetActive(false);
            FadeManager.FadeOut(quizUiCanvasGroup, 1f);
        });
        SceneManager.LoadScene("Home");
    }

    /// <summary>
    /// 次の問題へボタンが押された時に呼ばれるクラス
    /// </summary>
    private void OnClickUINextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 問題を読み込み、各テキストフィールドに文章を反映させるクラス
    /// </summary>
    /// <param name="data">問題データ</param>
    /// <param name="id">問題番号</param>
    private void SetQuestion(List<string[]> data, int id)
    {
        questionText.text = _questionData[id][0];
        for (var i = 0; i < answerButtonObjects.Length; i++) answerButtonObjects[i].GetComponentInChildren<Text>().text = data[id][i + 1];
    }
    
    /// <summary>
    /// クイズに答えて、それが正解か不正解かをboolで返す関数
    /// </summary>
    /// <param name="data">問題データ</param>
    /// <param name="id">問題番号</param>
    /// <param name="buttonID">押されたボタンに対応するID</param>
    /// <returns></returns>
    private static bool AnswerQuestion(List<string[]> data, int id, int buttonID)
    {
        string[] answer = { "A", "B", "C", "D" ,"Time_out"};
        var isCorrect = ((data[id][5] == answer[buttonID] || data[id][5] == "X") && buttonID != 4);
        Debug.Log(isCorrect ? "正解" : "不正解");
        return isCorrect;
    }

    /// <summary>
    /// 正しい選択肢のボタンをハイライトするクラス
    /// </summary>
    /// <param name="data">クイズデータ</param>
    /// <param name="questionID">問題番号</param>
    private void HighlightCorrectAnswer(List<string[]> data, int questionID)
    {
        foreach (var t in _answerButtons) t.interactable = false;
        
        var correctAnswer = data[questionID][5];
        switch (correctAnswer)
        {
            case "A":
                _answerButtons[0].interactable = true;
                break;
            case "B":
                _answerButtons[1].interactable = true;
                break;
            case "C":
                _answerButtons[2].interactable = true;
                break;
            case "D":
                _answerButtons[3].interactable = true;
                break;
            case "X":
                foreach (var t in _answerButtons) t.interactable = true;
                break;
        }
    }

    /// <summary>
    /// タイマーとタイムリミットを表示するバーの制御をするクラス
    /// </summary>
    /// <param name="seconds">タイマーの秒数を指定</param>
    private void TimeLimitCounter(float seconds)
    {
        _countTime += Time.deltaTime;
        _progress = _countTime / seconds;
        timeBarImage.fillAmount = _progress;
        if(_progress >= 1f)OnClickAnswerButton(4);
    }

    /// <summary>
    /// オーディオ再生制御クラス
    /// </summary>
    /// <param name="i">quizAudioSourcesの何番目のクリップを再生するか</param>
    /// <param name="play">再生を止める場合は”false”を渡す</param>
    private void AudioPlayer(int i, bool play = true)
    {
        if (play) quizAudioSource.PlayOneShot(quizAudioClips[i]);
        else quizAudioSource.Stop();
    }

    /// <summary>
    /// 指定された秒数後に任意のアクションを実行するクラス
    /// </summary>
    /// <param name="seconds">待つ秒数</param>
    /// <param name="callback">実行したい任意のアクション</param>
    private static async UniTask DelayAsync(float seconds, UnityAction callback)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
        callback?.Invoke();
    }
}