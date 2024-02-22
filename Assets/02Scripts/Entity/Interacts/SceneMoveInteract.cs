using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveInteract : InteractBase
{
    [SerializeField]
    private SCENE targetScene;

    public override void Interact()
    {
        GameManager.Inst.CurPlayer.Controllable = false;

        GameManager.Inst.SceneMove(targetScene);
    }
}
