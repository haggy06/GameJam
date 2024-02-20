using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoSingleton<PopupManager>
{
    #region _Popups_
    // ↓ 이런 식으로 팝업매니저 내의 주요 팝업들을 관리한다(해당되는 팝업을 Inspector 창에서 넣어줘야 함).
    [SerializeField]
    private PopupBase popup01;
    public PopupBase Popup01 => popup01;
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC를 눌렀을 경우
        {
            if (currentPopup != null) // 열려있는 팝업이 있었을 경우
            {
                PopupFadeOut();
            }
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            Debug.Log("가로 이동");
        }
        if (Input.GetButtonDown("Vertical"))
        {
            Debug.Log("세로 이동");
        }
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
