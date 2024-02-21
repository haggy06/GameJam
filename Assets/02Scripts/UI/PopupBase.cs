using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PopupBase : MonoBehaviour
{
    protected CanvasGroup popup;

    [SerializeField]
    protected ButtonNode firstButton;

    [SerializeField]
    protected bool closeWithESC = true;
    public bool CloseWithESC => closeWithESC;
    [SerializeField]
    protected float duration = 0.5f;

    [Space(10)]

    [SerializeField]
    protected PopupBase ownerPopup;
    public PopupBase OwnerPopup => ownerPopup;

    protected virtual void Awake()
    {
        popup = GetComponent<CanvasGroup>();
    }
    private void CanvasAct(bool isOn)
    {
        popup.interactable = isOn;
        popup.blocksRaycasts = isOn;
    }

    #region _Popup Logic_
    public void PopupFadeIn(PopupBase ownerPopup)
    {
        OnActive();

        this.ownerPopup = ownerPopup;
        LeanTween.value(popup.alpha, 1f, duration).setEase(LeanTweenType.linear).setOnUpdate((float value) => popup.alpha = value).setOnComplete(FadeInFinish).setIgnoreTimeScale(true); ;

        CanvasAct(true);
    }
    public void PopupFadeOut()
    {
        OnDeActive();

        LeanTween.value(popup.alpha, 0f, duration).setEase(LeanTweenType.linear).setOnUpdate((float value) => popup.alpha = value).setOnComplete(FadeOutFinish).setIgnoreTimeScale(true); ;

        CanvasAct(false);
    }

    public void PopupFadeIn_Again()
    {
        OnActive();

        LeanTween.value(popup.alpha, 1f, duration).setEase(LeanTweenType.linear).setOnUpdate((float value) => popup.alpha = value).setOnComplete(FadeInFinish).setIgnoreTimeScale(true); ;

        CanvasAct(true);
    }
    


    public void PopupShow(PopupBase ownerPopup)
    {
        OnActive();

        this.ownerPopup = ownerPopup;
        popup.alpha = 1f;

        CanvasAct(true);
    }
    public void PopupHide()
    {
        OnDeActive();

        popup.alpha = 0f;

        CanvasAct(false);
    }

    public void PopupShow_Again()
    {
        OnActive();

        popup.alpha = 1f;

        CanvasAct(true);
    }
    #endregion

    #region _Virtual Fuctions_
    protected virtual void FadeInFinish()
    {

    }
    protected virtual void FadeOutFinish()
    {

    }

    protected virtual void OnActive()
    {
        PopupManager.Inst.ResetButton(firstButton);
    }
    protected virtual void OnDeActive()
    {
        PopupManager.Inst.ResetButton(null);
    }
    #endregion
}
