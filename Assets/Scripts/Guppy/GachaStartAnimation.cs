using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Rendering;

public class GachaStartAnimation : MonoBehaviour
{
    public CubismRenderController gachaRenderController;
    public Animator gachaAnimAnimator;

    public void UpdateState(GachaState state)
    {
        if(state == GachaState.WAIT)
        {
            gachaRenderController.Opacity = 1f;
            gachaAnimAnimator.SetBool("Start", false);
        }
        else if(state == GachaState.START)
        {
            gachaRenderController.Opacity = 1f;
            gachaAnimAnimator.SetBool("Start", true);
        }
        else
        {
            gachaRenderController.Opacity = 0f;
            gachaAnimAnimator.SetBool("Start", false);
        }

    }
}
