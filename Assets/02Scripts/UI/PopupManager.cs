using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupManager : MonoSingleton<PopupManager>
{
    [SerializeField]
    private VirtualButton virtualButton;
    public VirtualButton VirtualButton => virtualButton;

    [Space(5)]

    #region _Popups_
    [Header("Popups")]

    // �� �̷� ������ �˾��Ŵ��� ���� �ֿ� �˾����� �����Ѵ�(�ش�Ǵ� �˾��� Inspector â���� �־���� ��).
    [SerializeField]
    private FadePopup fade;
    public FadePopup Fade => fade;


    [SerializeField]
    private PopupBase escPopup;
    public PopupBase ESCPopup => escPopup;


    [SerializeField]
    private InteractPopup interactPopup;
    public InteractPopup Interact_Popup => interactPopup;


    [SerializeField]
    private ManualPopup manualPopup;
    public ManualPopup Manual_Popup => manualPopup;
    #endregion
    private void Init() // ĵ������ �����Ǿ��� ���� ���� �̵��Ǿ��� �� �� �ʱ�ȭ�� �� ����� �Լ�
    {
        canOpenESC = (SceneManager.GetActiveScene().buildIndex > (int)SCENE.StageSelect); // ���� ���� Intro(0), Loading(1), Title(2), StageSelect(3) ���� �ƴ� ��� TRUE

        fade.PopupShow(null);
        fade.PopupFadeOut();

        escPopup.PopupHide();

        interactPopup.PopupHide();

        manualPopup.PopupHide();
    }

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

    private void Start()
    {
        Init();
    }

    [SerializeField]
    private bool canOpenESC = true;
    public bool CanOpenESC
    {
        get => canOpenESC;
        set => canOpenESC = value;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC�� ������ ���
        {
            if (currentPopup != null && currentPopup.CloseWithESC) // �����ִ� �˾��� �־��� esc�� ���� �� ���� ���
            {
                PopupFadeOut();
            }
            else if (canOpenESC) // �Ͻ������� ������ ���
            {
                PopupFadeIn_Override(escPopup, currentPopup);
            }
            else // �Ͻ������� �Ұ����� ���
            {
                Debug.Log("�Ͻ������� �� �� �����ϴ�.");
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

    public void NewSceneLoaded()
    {
        Init();
    }

    #region _Popup Fade In/Out Logic_
    [SerializeField]
    private PopupBase currentPopup;
    public PopupBase CurrentPopup => currentPopup;
    public void CurPopupReset()
    {
        currentPopup = null;
    }
    public void PopupFadeIn(PopupBase openPopup, PopupBase ownerPopup)
    {
        if (currentPopup == openPopup) // �� �˾��� �� ���� ���� ���� ���
        {
            Debug.Log("���õ� �˾��� �̹� ���� �ֽ��ϴ�");
        }
        else // �� �˾��� �� ���� ���� ���� ���� ���
        {
            if (currentPopup != null)
            {
                currentPopup.PopupFadeOut();
            }

            currentPopup = openPopup;
            currentPopup.PopupFadeIn(ownerPopup);
        }
    }
    public void PopupFadeIn_Override(PopupBase openPopup, PopupBase ownerPopup)
    {
        if (currentPopup == openPopup) // �� �˾��� �� ���� ���� ���� ���
        {
            Debug.Log("���õ� �˾��� �̹� ���� �ֽ��ϴ�");
        }
        else // �� �˾��� �� ���� ���� ���� ���� ���
        {
            currentPopup = openPopup;
            currentPopup.PopupFadeIn(ownerPopup);
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
