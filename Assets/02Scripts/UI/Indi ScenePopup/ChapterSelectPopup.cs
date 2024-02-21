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

        GetComponent<Button>().onClick.AddListener(ChapterButtonEvent);


        chapterArr.localPosition = Vector3.right * (((curChapterIndex) * -400f) - 175f);
        ChapterHighlight();


        PopupManager.Inst.PopupFadeIn(this, null);
    }
    private void ChapterHighlight()
    {
        if (!chapterArr.GetChild(curChapterIndex).TryGetComponent<ChapterInfo>(out curChapter))
        {
            Debug.Log("ChapterInfo 참조 실패");
        }
        chapterName.text = curChapter.ChapterName;

        curChapter.ChapterHighlighted();
    }


    private void ChapterButtonEvent()
    {
        if (curChapter != null)
        {
            curChapter.ChapterSelect();
        }
    }

    [SerializeField]
    private int curChapterIndex = 0;
    private int input;

    [SerializeField]
    private ChapterInfo curChapter;
    private void Update()
    {
        
        if (controllable)
        {
            if (Input.GetButtonDown("Horizontal")) // 입력이 존재했을 경우
            {
                Debug.Log("이동 감지 : " + input);

                input = (int)Input.GetAxisRaw("Horizontal");
                curChapterIndex += input;

                if (curChapterIndex < chapterArr.childCount && curChapterIndex >= 0) // 이동했을 때 인덱스오버가 안 날 경우
                {
                    LeanTween.moveLocalX(chapterArr.gameObject, ((curChapterIndex) * -400f) - 175f, lerpTime).setEase(LeanTweenType.easeOutQuart);

                    ChapterHighlight();
                }
                else // 인덱스 오버가 날 경우
                {
                    curChapterIndex -= input;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChapterButtonEvent();
            }
        }
    }

    private bool controllable;
    protected override void OnActive()
    {
        base.OnActive();

        controllable = true;
    }
    protected override void OnDeActive()
    {
        base.OnDeActive();

        controllable = false;
    }
}
