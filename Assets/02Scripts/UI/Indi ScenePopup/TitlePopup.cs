using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TitlePopup : PopupBase
{
    [Space(10)]

    [SerializeField]
    private Button startButton;

    protected override void Awake()
    {
        base.Awake();

        startButton.onClick.AddListener(() => GameManager.Inst.SceneMove(SCENE.StageSelect));
        PopupManager.Inst.ResetButton(firstButton);
    }
}
