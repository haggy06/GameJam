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

    // ↓ 이런 식으로 팝업매니저 내의 주요 팝업들을 관리한다(해당되는 팝업을 Inspector 창에서 넣어줘야 함).
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
    private void Init() // 캔버스가 생성되었을 때나 씬이 이동되었을 때 등 초기화할 때 사용할 함수
    {
        canOpenESC = (SceneManager.GetActiveScene().buildIndex > (int)SCENE.StageSelect); // 현재 씬이 Intro(0), Loading(1), Title(2), StageSelect(3) 씬이 아닐 경우 TRUE

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
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC를 눌렀을 경우
        {
            if (currentPopup != null && currentPopup.CloseWithESC) // 열려있는 팝업이 있었고 esc로 닫을 수 있을 경우
            {
                PopupFadeOut();
            }
            else if (canOpenESC) // 일시정지가 가능할 경우
            {
                PopupFadeIn_Override(escPopup, currentPopup);
            }
            else // 일시정지가 불가능할 경우
            {
                Debug.Log("일시정지를 할 수 없습니다.");
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
        if (currentPopup == openPopup) // 열 팝업이 맨 위에 열려 있을 경우
        {
            Debug.Log("선택된 팝업이 이미 열려 있습니다");
        }
        else // 열 팝업이 맨 위에 열려 있지 않을 경우
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
        if (currentPopup == openPopup) // 열 팝업이 맨 위에 열려 있을 경우
        {
            Debug.Log("선택된 팝업이 이미 열려 있습니다");
        }
        else // 열 팝업이 맨 위에 열려 있지 않을 경우
        {
            currentPopup = openPopup;
            currentPopup.PopupFadeIn(ownerPopup);
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
