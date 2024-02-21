using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SceneMoveButton : ButtonNode
{
    [Header("Scene Name")]
    [SerializeField]
    private SCENE targetScene;
    public SCENE TargetScene => targetScene;
    public void ChangeTargetScene(SCENE newScene)
    {
        targetScene = newScene;
    }


    protected override void Awake()
    {
        base.Awake();

        GetComponent<Button>().onClick.AddListener(() => GameManager.Inst.SceneMove(targetScene));
    }
}
