using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveInteract : InteractBase
{
    [SerializeField]
    private SCENE targetScene;

    public override void Interact()
    {
        base.Interact();

        GameManager.Inst.CurPlayer.Controllable = false;

        AudioManager.Inst.PlaySFX(SFX.Portal);
        GameManager.Inst.SceneMove(targetScene);
    }
}
