using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;


public class LoadingScreen : MonoBehaviour
{
    public Text loadingText;
    private string _text = "Loading";
    private int _step = 0;
    
    private void OnEnable()
    {
        Observable.Interval(TimeSpan.FromSeconds(0.4f)).TakeUntilDisable(this).Subscribe(_ => TextController());
        //ToDo:横にサンマが跳ねてるLive2Dを入れたら見栄えが良さそう
    }

    private void OnDisable()
    {
        loadingText.text = _text = "Loading";
    }

    /// <summary>
    /// テキストを動かす関数
    /// </summary>
    private void TextController()
    {
        _text += ".";
        _step++;
        if (_step == 4)
        {
            _step = 0;
            _text = "Loading";
        }
        loadingText.text = _text;
    }
}
