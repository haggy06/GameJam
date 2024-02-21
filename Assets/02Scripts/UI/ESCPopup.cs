using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ESCPopup : PopupBase
{
    [Header("Buttons")]
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Button goToMainButton;

    private void Start()
    {
        continueButton.onClick.AddListener(PopupManager.Inst.PopupFadeOut);

        retryButton.onClick.AddListener(() => GameManager.Inst.SceneMove(GameManager.Inst.CurScene));
        retryButton.onClick.AddListener(PopupFadeOut);

        goToMainButton.onClick.AddListener(() => GameManager.Inst.SceneMove(SCENE.Title));
        goToMainButton.onClick.AddListener(PopupFadeOut);
    }

    protected override void FadeOutFinish()
    {
        if (PopupManager.Inst.CurrentPopup == this)
        {
            PopupManager.Inst.CurPopupReset();
        }
    }

    protected override void OnActive()
    {
        base.OnActive();

        GameManager.Inst.THEWORLD(true);
    }
    protected override void OnDeActive()
    {
        base.OnDeActive();

        GameManager.Inst.THEWORLD(false);
    }
}
