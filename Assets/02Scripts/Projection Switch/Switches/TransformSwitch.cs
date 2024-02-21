using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSwitch : ProjectionBase
{
    [Header("Ortho Transform")]
    [SerializeField]
    private Vector3 orthoPosition;
    [SerializeField]
    private Vector3 orthoRotation;
    [SerializeField]
    private Vector3 orthoSize;

    [Header("Persp Transform")]
    [SerializeField]
    private Vector3 perspPosition;
    [SerializeField]
    private Vector3 perspRotation;
    [SerializeField]
    private Vector3 perspSize;


    public override void ToOrthoStart()
    {
        LeanTween.move(gameObject, orthoPosition, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
        LeanTween.rotate(gameObject, orthoRotation, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
        LeanTween.scale(gameObject, orthoSize, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
    }
    public override void ToPerspStart()
    {
        LeanTween.move(gameObject, perspPosition, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
        LeanTween.rotate(gameObject, perspRotation, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
        LeanTween.scale(gameObject, perspSize, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
    }
}
