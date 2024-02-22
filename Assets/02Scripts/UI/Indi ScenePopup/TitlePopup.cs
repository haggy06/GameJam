using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TitlePopup : PopupBase
{
    [Space(10)]

    [SerializeField]
    private PopupBase cutScenePopup;

    protected override void Awake()
    {
        base.Awake();

        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => GameManager.Inst.SceneMove(SCENE.StageSelect));
        transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => PopupManager.Inst.PopupFadeIn(cutScenePopup, this));
        transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => PopupManager.Inst.Manual_Popup.SetManualImage("Control"));


        StartCoroutine("SetButtonNode");
    }/*
    private void Start()
    {
        PopupManager.Inst.PopupFadeIn(this, null);
    }*/

    private IEnumerator SetButtonNode()
    {
        yield return null;

        PopupManager.Inst.PopupFadeIn(this, null);
        //PopupManager.Inst.ResetButton(firstButton);
    }
}
