using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoSingleton<PopupManager>
{
    #region _Popups_
    // �� �̷� ������ �˾��Ŵ��� ���� �ֿ� �˾����� �����Ѵ�(�ش�Ǵ� �˾��� Inspector â���� �־���� ��).
    [SerializeField]
    private PopupBase popup01;
    public PopupBase Popup01 => popup01;
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC�� ������ ���
        {
            if (currentPopup != null) // �����ִ� �˾��� �־��� ���
            {
                PopupFadeOut();
            }
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            Debug.Log("���� �̵�");
        }
        if (Input.GetButtonDown("Vertical"))
        {
            Debug.Log("���� �̵�");
        }
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
