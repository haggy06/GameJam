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

        //GetComponent<Button>().onClick.AddListener(ChapterButtonEvent);


        chapterArr.localPosition = Vector3.right * (((curChapterIndex + 1) * -550f) - 250f);
        ChapterHighlight();

        PopupManager.Inst.ResetButton(firstButton);
    }

    public void AimToIndex(int index)
    {
        LeanTween.moveLocalX(chapterArr.gameObject, ((index) * -550f) - 250f, lerpTime).setEase(LeanTweenType.easeOutQuart);

        curChapterIndex = index;

        ChapterHighlight();
    }

    private void ChapterHighlight()
    {
        if (curChapterIndex >=0 && curChapterIndex < chapterArr.childCount)
        {
            if (!chapterArr.GetChild(curChapterIndex).TryGetComponent<ChapterButton>(out curChapter))
            {
                Debug.Log("ChapterInfo 참조 실패");
            }

            chapterName.text = curChapter.ChapterName;
            this.backgroundColors = curChapter.BackgroundColors;
        }
    }

    [SerializeField]
    private Color[] backgroundColors;
    [SerializeField]
    private Image background;
    [SerializeField]
    private float swapTime = 2f;


    private void Start()
    {
        BackgroundColorChange(1);

        PopupManager.Inst.ResetButton(firstButton);
        Invoke("ButtonSet", 0.02f);  
    }
    private void ButtonSet()
    {
        PopupManager.Inst.PopupFadeIn(this, null);
    }
    private void BackgroundColorChange(int colorIndex)
    {
        LeanTween.value(background.gameObject, background.color, backgroundColors[colorIndex], swapTime).setOnUpdate((Color value) => background.color = value).setOnComplete(() => BackgroundColorChange(ColorIndexChange(colorIndex)));
    }
    private int ColorIndexChange(int i)
    {
        i++;

        if (i >= 3) // 컬러 인덱스를 초과했을 경우
        {
            Debug.Log("컬러 인덱스 변경 : " + 0);

            return 0;
        }
        else
        {
            Debug.Log("컬러 인덱스 변경 : " + i);

            return i;
        }
    }

    /*
    private void ChapterButtonEvent()
    {
        if (curChapter != null)
        {
            curChapter.ChapterSelect();
        }
    }
    */

    [SerializeField]
    private int curChapterIndex = 0;
    private int input;

    [SerializeField]
    private ChapterButton curChapter;
    private void Update()
    {/*
        
        if (controllable)
        {
            if (Input.GetButtonDown("Horizontal")) // 입력이 존재했을 경우
            {
                Debug.Log("이동 감지 : " + input);

                input = (int)Input.GetAxisRaw("Horizontal");
                curChapterIndex += input;

                if (curChapterIndex < chapterArr.childCount && curChapterIndex >= 0) // 이동했을 때 인덱스오버가 안 날 경우
                {
                    LeanTween.moveLocalX(chapterArr.gameObject, ((curChapterIndex) * -550f) -250f, lerpTime).setEase(LeanTweenType.easeOutQuart);

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
            
        }*/
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
