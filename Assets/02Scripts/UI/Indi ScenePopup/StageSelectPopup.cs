using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using System.IO;

public class StageSelectPopup : PopupBase
{    
    [Header("StagePopup Components")]

    [SerializeField]
    private TextMeshProUGUI chapterName;
    [SerializeField]
    private Image background;

    [SerializeField]
    private GameObject stage1;
    [SerializeField]
    private GameObject stage2;


    [Space(5)]

    [SerializeField]
    private Color[] backgroundColors;
    [SerializeField]
    private float swapTime = 1f;

    public void StagePopupSetting(string chapterName, Color[] backgroundColors, SCENE stage1_Scene, SCENE stage2_Scene)
    {
        this.backgroundColors = backgroundColors;

        this.chapterName.text = chapterName;

        stage1.GetComponent<Image>().sprite = Resources.Load<Sprite>(Path.Combine("StagePreview", stage1_Scene.ToString()));
        stage1.GetComponent<SceneMoveButton>().ChangeTargetScene(stage1_Scene);

        stage2.GetComponent<Image>().sprite = Resources.Load<Sprite>(Path.Combine("StagePreview", stage2_Scene.ToString()));
        stage2.GetComponent<SceneMoveButton>().ChangeTargetScene(stage2_Scene);
    }


    private void Start()
    {
        BackgroundColorChange(1);
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
}
