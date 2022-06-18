using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class OverlayContentViewerController : MonoBehaviour
{
    public GameObject contentsObject;
    public Button closeButton;
    private Vector3 _contentInitPosition;

    private void Start()
    {
        _contentInitPosition = contentsObject.transform.position;
    }

    private void OnEnable()
    {
        contentsObject.transform.position = _contentInitPosition;
        closeButton.OnClickAsObservable().First().Subscribe(_ => OnClickCloseButton()).AddTo(this);
    }

    private void OnClickCloseButton()
    {
        gameObject.SetActive(false);
    }
}
