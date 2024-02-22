using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitch : ProjectionBase
{
    [SerializeField]
    private Color perspColor;
    public Color PerspColor
    {
        get => perspColor;
        set => perspColor = value;
    }
    [SerializeField]
    private Color orthoColor;
    public Color OrthoColor
    {
        get => orthoColor;
        set => orthoColor = value;
    }

    

    public override void ToOrthoStart()
    {
        LeanTween.color(gameObject, orthoColor, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
    }
    public override void ToPerspStart()
    {
        LeanTween.color(gameObject, perspColor, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
    }
}
