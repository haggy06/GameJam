using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSwitch : ProjectionBase
{
    [SerializeField, Tooltip("{position, rotation, size}")]
    private Vector3[] perspTransform = new Vector3[3];

    [SerializeField, Tooltip("{position, rotation, size}")]
    private Vector3[] orthoTransform = new Vector3[3];


    public override void ToOrthoStart()
    {
        LeanTween.move(gameObject, orthoTransform[0], ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
        LeanTween.rotate(gameObject, orthoTransform[1], ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
        LeanTween.scale(gameObject, orthoTransform[2], ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
    }
    public override void ToPerspStart()
    {
        LeanTween.move(gameObject, perspTransform[0], ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
        LeanTween.rotate(gameObject, perspTransform[1], ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
        LeanTween.scale(gameObject, perspTransform[2], ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
    }
}
