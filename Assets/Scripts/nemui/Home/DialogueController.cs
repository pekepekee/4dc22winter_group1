using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class DialogueController : MonoBehaviour
{
  [SerializeField] private Button screenTouchArea = default;
  [SerializeField] private TextAsset dialogueTextAsset = default;

  private string[] dialogueDataArray;
  private string dialogueData;
  private Text dialogueText;
  void Start()
  {
    dialogueData = dialogueTextAsset.text;
    dialogueDataArray = dialogueData.Split('\n');

    dialogueText = this.gameObject.transform.GetChild(0).GetComponent<Text>();

    screenTouchArea = screenTouchArea.GetComponent<Button>();
    screenTouchArea.OnClickAsObservable()
                       .Subscribe(_ =>
                         OnClickScreenTouchArea()
                       )
                       .AddTo(this);

    OnClickScreenTouchArea(); // はじめに一回だけ実行する
  }


  /// <summary>
  /// 画面をクリックしたときにランダムでセリフを吐くクラス
  /// </summary>
  private void OnClickScreenTouchArea()
  {
    dialogueText.text = dialogueDataArray[Random.Range(0, dialogueDataArray.Length)];
  }
}
