using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaAnimation : MonoBehaviour
{
    public Transform targetTransform;
    public AudioSource sanmaSE;
    public AudioSource magroSE;

    private bool gachaAnimPlaying;
    private Queue<GachaItem> gachaResults;
    private float animDuration = 0.0f;
    private GachaItem currentGachaItem;

    private GameObject currentLive2DObject;

    // Start is called before the first frame update
    void Start()
    {
        gachaResults = new Queue<GachaItem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gachaAnimPlaying)
        {
            if(animDuration <= 0.0f)
            {
                gachaAnimPlaying = false;
            }
            animDuration -= Time.deltaTime;
        }
    }

    public void AddGachaItem(GachaItem item)
    {
        gachaResults.Enqueue(item);
    }

    public void Next()
    {
        if(HasNext())
        {
            gachaAnimPlaying = true;
            currentGachaItem = gachaResults.Dequeue();
            animDuration = currentGachaItem.live2DShowDuration;
            SetLive2DObject(currentGachaItem.live2DModel);
            if (currentGachaItem.isGameClearItem)
            {
                magroSE.PlayOneShot(magroSE.clip);
            }
            else
            {
                sanmaSE.PlayOneShot(sanmaSE.clip);
            }
        }
        else
        {
            FinishAnimation();
        }
    }

    public void Skip()
    {
        // そもそもアイテムが存在しない場合はreturn
        // (謎のエラーでゲームが動かなくなったことが合ったので対策)
        if(currentGachaItem == null)
        {
            return;
        }
        // マグロのみスキップ不可
        if (currentGachaItem.isGameClearItem == true)
        {
            return;
        }
        Next();
    }

    public void FinishAnimation()
    {
        gachaAnimPlaying = false;
        ClearLive2DObject();
    }

    public bool IsAnimPlaying()
    {
        return gachaAnimPlaying;
    }

    public bool HasNext()
    {
        return gachaResults.Count > 0;
    }

    public void SkipAll()
    {
        gachaResults.Clear();
    }

    private void SetLive2DObject(GameObject live2DObject)
    {
        if (currentLive2DObject)
        {
            Destroy(currentLive2DObject);
        }
        currentLive2DObject = Instantiate(live2DObject, targetTransform);
    }

    private void ClearLive2DObject()
    {
        Destroy(currentLive2DObject);
        currentLive2DObject = null;
    }
}
