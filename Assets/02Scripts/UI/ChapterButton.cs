using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChapterButton : ButtonNode
{
    [Header("Chapter Information")]

    [SerializeField]
    private string chapterName;
    public string ChapterName => chapterName;

    [SerializeField]
    private Color[] backgroundColors = new Color[3];
    public Color[] BackgroundColors => backgroundColors;

    [Space(5)]

    [SerializeField]
    private SCENE stage;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GameManager.Inst.SceneMove(stage));
    }

    [SerializeField]
    private int chapterIndex = 0;
    [SerializeField]
    private ChapterSelectPopup chapterSelectPopup;
    public override void Highlighted()
    {
        base.Highlighted();

        chapterSelectPopup.AimToIndex(chapterIndex);
    }
}
