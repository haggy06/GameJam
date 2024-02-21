using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterInfo : MonoBehaviour
{
    [SerializeField]
    private StageSelectPopup chapterPopup;

    [Header("Chapter Information")]

    [SerializeField]
    private string chapterName;
    public string ChapterName => chapterName;

    [SerializeField]
    private Color[] backgroundColors = new Color[3];
    
    [Space(5)]

    [SerializeField]
    private SCENE stage1;
    [SerializeField]
    private SCENE stage2;

    public void ChapterSelect()
    {
        if (chapterPopup != null)
        {
            PopupManager.Inst.PopupFadeIn(chapterPopup, transform.parent.parent.parent.GetComponent<PopupBase>());
        }
        else
        {
            Debug.LogWarning(gameObject.name + "에 연결된 챕터가 없습니다");
        }
    }

    public void ChapterHighlighted()
    {
        chapterPopup.StagePopupSetting(chapterName, backgroundColors, stage1, stage2);
    }
}
