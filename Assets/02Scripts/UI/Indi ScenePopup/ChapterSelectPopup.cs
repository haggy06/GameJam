using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class ChapterSelectPopup : PopupBase
{
    [Space(10)]

    [SerializeField]
    private float lerpTime = 0.25f;

    [Space(5)]

    [SerializeField]
    private RectTransform chapterArr;
    [SerializeField]
    private TextMeshProUGUI chapterName;

    protected override void Awake()
    {
        base.Awake();

        chapterArr.GetChild(curChapterIndex).GetComponent<ChapterInfo>();

        PopupManager.Inst.PopupFadeIn(this, null);
    }

    [SerializeField]
    private int curChapterIndex = 0;
    private int input;

    [SerializeField]
    private ChapterInfo curChapter;
    private void Update()
    {
        
        if (Input.GetButtonDown("Horizontal")) // �Է��� �������� ���
        {
            Debug.Log("�̵� ���� : " + input);

            input = (int)Input.GetAxisRaw("Horizontal");
            curChapterIndex += input;

            if (curChapterIndex < chapterArr.childCount && curChapterIndex >= 0) // �̵����� �� �ε��������� �� �� ���
            {
                LeanTween.moveLocalX(chapterArr.gameObject, ((curChapterIndex) * -400f), lerpTime)/*.setEase(LeanTweenType.easeOutQuart)*/;

                chapterArr.GetChild(curChapterIndex).GetComponent<ChapterInfo>();
            }
            else // �ε��� ������ �� ���
            {
                curChapterIndex -= input;
            }
        }
    }
}
