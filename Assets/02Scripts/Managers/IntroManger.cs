using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManger : MonoBehaviour
{
    [SerializeField]
    private float waitTime = 1f;
    private void Awake()
    {
        PopupManager.Inst.Fade.PopupFadeOut();

        Invoke("GoTitle", waitTime);
    }

    private void GoTitle()
    {
        GameManager.Inst.SceneMove(SCENE.Title);
    }
}
