using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSwitch : ProjectionBase
{
    [SerializeField, Tooltip("{position, rotation, size}")]
    private Vector3[] perspTransform = new Vector3[3];

    [SerializeField, Tooltip("{position, rotation, size}")]
    private Vector3[] orthoTransform = new Vector3[3];

    [Header("Change Decide")]

    [SerializeField]
    private bool positionChange = true;
    [SerializeField]
    private bool rotationChange = true;
    [SerializeField]
    private bool sizeChange = true;
    public override void ToOrthoStart()
    {
        if (positionChange)
            LeanTween.moveLocal(gameObject, orthoTransform[0], ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);

        if (rotationChange)
            LeanTween.rotateLocal(gameObject, orthoTransform[1], ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);

        if (sizeChange)
            LeanTween.scale(gameObject, orthoTransform[2], ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
    }
    public override void ToPerspStart()
    {
        if (positionChange)
            LeanTween.moveLocal(gameObject, perspTransform[0], ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);

        if (rotationChange)
            LeanTween.rotateLocal(gameObject, perspTransform[1], ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);

        if (sizeChange)
            LeanTween.scale(gameObject, perspTransform[2], ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
    }
}
