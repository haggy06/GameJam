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
    // �� �̷� ������ �˾��Ŵ��� ���� �ֿ� �˾����� �����Ѵ�(�ش�Ǵ� �˾��� Inspector â���� �־���� ��).
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
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC�� ������ ���
        {
            if (currentPopup != null) // �����ִ� �˾��� �־��� ���
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
        Debug.Log("�˾� �Ŵ��� ����");

        fade.PopupShow(null);
        fade.PopupFadeOut();
    }

    #region _Popup Fade In/Out Logic_
    private PopupBase currentPopup;
    public PopupBase CurrentPopup => currentPopup;
    public void PopupFadeIn(PopupBase openPopup, PopupBase ownerPopup)
    {
        if (currentPopup == openPopup) // �� �˾��� �� ���� ���� ���� ���
        {
            Debug.Log("���õ� �˾��� �̹� ���� �ֽ��ϴ�");
        }
        else // �� �˾��� �� ���� ���� ���� ���� ���
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

        if (currentPopup.OwnerPopup == null) // ���� �˾��� ������ �˾��� ���� ���
        {
            currentPopup = null;
        }
        else // ���� �˾��� ������ �˾��� ���� ���
        {
            currentPopup.OwnerPopup.PopupFadeIn_Again();

            currentPopup = currentPopup.OwnerPopup;
        }
    }
    #endregion
}
