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
    private List<ProjectionBase> projections = new List<ProjectionBase>();
    public List<ProjectionBase> Projections => projections;

    public void InputList(ProjectionBase projBase)
    {
        projections.Add(projBase);
    }

    [SerializeField]
    private LeanTweenType leanType = LeanTweenType.easeOutCirc;
    public LeanTweenType LeanType => leanType;
    public void ProjectionChange()
    {
        if (orthographic) // 현재 Orthographic 시점이었을 경우
        {
            orthographic = false;

            for (int i = 0; i < projections.Count; i++)
            {
                if (projections[i].Changeable)
                    projections[i].ToPerspStart();
            }
            Invoke("ToPresp", duration);
            /*
            //LeanTween.moveZ(backgroundObj, 0f, duration).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scaleZ(mapObj, 1f, duration).setEase(leanType).setOnComplete(ToPresp);

            LeanTween.move(cameraObj, targetPosition, duration).setEase(leanType);
            LeanTween.rotate(cameraObj, targetRotation, duration).setEase(leanType);
            */
        }
        else // 현재 Perspective 시점이었을 경우
        {
            orthographic = true;

            for (int i = 0; i < projections.Count; i++)
            {
                if (projections[i].Changeable)
                    projections[i].ToOrthoStart();
            }
            Invoke("ToOrtho", duration);
            /*
            //LeanTween.moveZ(backgroundObj, -1f, duration).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scaleZ(mapObj, 0.0001f, duration).setEase(leanType).setOnComplete(ToOrtho);

            LeanTween.move(cameraObj, Vector3.back * 15.585f, duration).setEase(leanType);
            //LeanTween.move(cameraObj, Vector3.back * 21.7f, duration).setEase(leanType);


            LeanTween.rotate(cameraObj, Vector3.zero, duration).setEase(leanType);
            */
        }
    }

    private void ToOrtho()
    {
        for (int i = 0; i < projections.Count; i++)
        {
            if (projections[i].Changeable)
                projections[i].ToOrthoComplete();
        }
    }
    private void ToPresp()
    {
        for (int i = 0; i < projections.Count; i++)
        {
            if (projections[i].Changeable)
                projections[i].ToPerspComplete();
        }
    }


    public void ProjectionListClear()
    {
        projections.Clear();
    }
}
