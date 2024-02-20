using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PopupManager : MonoSingleton<PopupManager>
{
    [SerializeField]
    private VirtualButton virtualButton;
    public VirtualButton VirtualButton => virtualButton;


    #region _Popups_
    // ↓ 이런 식으로 팝업매니저 내의 주요 팝업들을 관리한다(해당되는 팝업을 Inspector 창에서 넣어줘야 함).
    [SerializeField]
    private FadePopup fade;
    public FadePopup Fade => fade;



    #endregion

    [Space(10)]

    [SerializeField]
    private ButtonNode curButton;
    public ButtonNode CurButton
    {
        get => curButton;
        set
        {
            if (curButton != null)
            {
                curButton.ButtonNormal();
            }

            curButton = value;

            if (curButton != null)
            {
                curButton.ButtonHighlight();
            }
        }
    }

    public void ResetButton(ButtonNode firstNode)
    {
        curButton = virtualButton;

        virtualButton.SetNode(firstNode);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC를 눌렀을 경우
        {
            if (currentPopup != null) // 열려있는 팝업이 있었을 경우
            {
                PopupFadeOut();
            }
        }

        if (curButton != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && curButton.Node_Up != null) CurButton = curButton.Node_Up;
            
            else if (Input.GetKeyDown(KeyCode.DownArrow) && curButton.Node_Down != null) CurButton = curButton.Node_Down;
            
            else if (Input.GetKeyDown(KeyCode.RightArrow) && curButton.Node_Right != null) CurButton = curButton.Node_Right;

            else if (Input.GetKeyDown(KeyCode.LeftArrow) && curButton.Node_Left != null) CurButton = curButton.Node_Left;

            if (Input.GetKeyDown(KeyCode.Space)) curButton.ButtonClick();
        }
    }

    public void NextSceneLoaded()
    {
        Debug.Log("팝업 매니저 리셋");

        fade.PopupShow(null);
        fade.PopupFadeOut();
    }

    #region _Popup Fade In/Out Logic_
    private PopupBase currentPopup;
    public PopupBase CurrentPopup => currentPopup;
    public void PopupFadeIn(PopupBase openPopup, PopupBase ownerPopup)
    {
        if (currentPopup == openPopup) // 열 팝업이 맨 위에 열려 있을 경우
        {
            Debug.Log("선택된 팝업이 이미 열려 있습니다");
        }
        else // 열 팝업이 맨 위에 열려 있지 않을 경우
        {
            openPopup.PopupFadeIn(ownerPopup);

            if (currentPopup != null)
            {
                currentPopup.PopupFadeOut();
            }
            currentPopup = openPopup;
        }
    }

    public void PopupFadeOut()
    {
        currentPopup.PopupFadeOut();

        if (currentPopup.OwnerPopup == null) // 현재 팝업을 열었던 팝업이 없을 경우
        {
            currentPopup = null;
        }
        else // 현재 팝업을 열었던 팝업이 있을 경우
        {
            currentPopup.OwnerPopup.PopupFadeIn_Again();

            currentPopup = currentPopup.OwnerPopup;
        }
    }
    #endregion
}
