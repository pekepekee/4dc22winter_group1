using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SpriteAnimator : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    [SerializeField] public Sprite[] sprites;
    [SerializeField] public float frameRate;
    
    private int _spritesCount;

    private void OnEnable()
    {
        Observable.Interval(TimeSpan.FromSeconds(1 / frameRate)).TakeUntilDisable(this).Subscribe(_ => SpriteImageAnimator());
    }

    private void OnDisable()
    {
        image.sprite = sprites[_spritesCount = 0];
    }

    private void SpriteImageAnimator()
    {
        if (_spritesCount >= sprites.Length) _spritesCount = 0;
        image.sprite = sprites[_spritesCount++];
    }
}
