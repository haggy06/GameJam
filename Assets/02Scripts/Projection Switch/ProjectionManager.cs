using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionManager : MonoSingleton<ProjectionManager>
{
    [SerializeField]
    private GameObject mapObj;
    [SerializeField]
    private GameObject cameraObj;

    [SerializeField]
    private float duration = 1f;
    public float Duration => duration;

    [Space(5)]

    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private Vector3 targetRotation;

    [SerializeField]
    private bool orthographic = true;
    public bool Orthographic => orthographic;

    [SerializeField]
    private List<ProjectionBase> rigidSwitches = new List<ProjectionBase>();
    public List<ProjectionBase> RigidSwitches => rigidSwitches;

    public void InputList(ProjectionBase projBase)
    {
        rigidSwitches.Add(projBase);
    }

    [SerializeField]
    private LeanTweenType leanType = LeanTweenType.easeOutCirc;
    public LeanTweenType LeanType => leanType;
    public void ProjectionChange()
    {
        if (orthographic) // ���� Orthographic �����̾��� ���
        {
            orthographic = false;

            for (int i = 0; i < rigidSwitches.Count; i++)
            {
                if (rigidSwitches[i].Changeable)
                    rigidSwitches[i].ToPerspStart();
            }

            //LeanTween.moveZ(backgroundObj, 0f, duration).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scaleZ(mapObj, 1f, duration).setEase(leanType).setOnComplete(ToPresp);

            LeanTween.move(cameraObj, targetPosition, duration).setEase(leanType);
            LeanTween.rotate(cameraObj, targetRotation, duration).setEase(leanType);
        }
        else // ���� Perspective �����̾��� ���
        {
            orthographic = true;

            for (int i = 0; i < rigidSwitches.Count; i++)
            {
                if (rigidSwitches[i].Changeable)
                    rigidSwitches[i].ToOrthoStart();
            }

            //LeanTween.moveZ(backgroundObj, -1f, duration).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scaleZ(mapObj, 0.001f, duration).setEase(leanType).setOnComplete(ToOrtho);

            LeanTween.move(cameraObj, Vector3.back * 15.585f, duration).setEase(leanType);
            //LeanTween.move(cameraObj, Vector3.back * 21.7f, duration).setEase(leanType);


            LeanTween.rotate(cameraObj, Vector3.zero, duration).setEase(leanType);
        }
    }

    private void ToOrtho()
    {
        for (int i = 0; i < rigidSwitches.Count; i++)
        {
            if (rigidSwitches[i].Changeable)
                rigidSwitches[i].ToOrthoComplete();
        }
    }
    private void ToPresp()
    {
        for (int i = 0; i < rigidSwitches.Count; i++)
        {
            if (rigidSwitches[i].Changeable)
                rigidSwitches[i].ToPerspComplete();
        }
    }
}