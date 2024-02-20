using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FadeType
{
    SceneMove,
    GameQuit,

}

public class FadePopup : PopupBase
{

    private FadeType curFadeType;
    public void Fade(FadeType fadeType)
    {
        curFadeType = fadeType;

        PopupFadeIn(null);
    }

    protected override void FadeInFinish()
    {
        switch (curFadeType)
        {
            case FadeType.SceneMove:
                
                break;


            case FadeType.GameQuit:

                break;
        }
    }
}
