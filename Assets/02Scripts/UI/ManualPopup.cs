using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;

public class ManualPopup : PopupBase
{
    [SerializeField]
    private Image manualImage;
    public void SetManualImage(string imageName)
    {
        if (Resources.Load<Sprite>(Path.Combine("Manuals", imageName)) != null)
        {
            manualImage.sprite = Resources.Load<Sprite>(Path.Combine("Manuals", imageName));

            PopupManager.Inst.PopupFadeIn_Override(this, PopupManager.Inst.CurrentPopup);
        }
        else
        {
            Debug.LogWarning(imageName + " 이란 매뉴얼 이미지가 없습니다.");
        }
    }
}
