using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitch : ProjectionBase
{
    [SerializeField]
    private Color orthoColor;

    [SerializeField]
    private Color perspColor;

    public override void ToOrthoStart()
    {
        LeanTween.color(gameObject, orthoColor, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
    }
    public override void ToPerspStart()
    {
        LeanTween.color(gameObject, perspColor, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
    }
}
