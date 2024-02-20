using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
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
    private List<RigidSwitch> rigidSwitches = new List<RigidSwitch>();

    public void InputList(RigidSwitch rig)
    {
        rigidSwitches.Add(rig);
    }
    public void InputList(Collider2D col2D)
    {

    }

    public void InputList(Collider col)
    {

    }
    public void ProjectionChange()
    {
        if (orthographic) // 현재 Orthographic 시점이었을 경우
        {
            orthographic = false;

            for (int i = 0; i < rigidSwitches.Count; i++)
            {
                rigidSwitches[i].ToPerspective();
            }

            //LeanTween.moveZ(backgroundObj, 0f, duration).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scaleZ(mapObj, 1f, duration).setEase(LeanTweenType.easeOutCirc);

            LeanTween.move(cameraObj, targetPosition, duration).setEase(LeanTweenType.easeOutCirc);
            LeanTween.rotate(cameraObj, targetRotation, duration).setEase(LeanTweenType.easeOutCirc);
        }
        else // 현재 Perspective 시점이었을 경우
        {
            orthographic = true;

            //LeanTween.moveZ(backgroundObj, -1f, duration).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scaleZ(mapObj, 0.001f, duration).setEase(LeanTweenType.easeOutCirc).setOnComplete(RigidON);

            LeanTween.move(cameraObj, Vector3.back * 15.585f, duration).setEase(LeanTweenType.easeOutCirc);
            LeanTween.rotate(cameraObj, Vector3.zero, duration).setEase(LeanTweenType.easeOutCirc);
        }
    }

    private void RigidON()
    {
        for (int i = 0; i < rigidSwitches.Count; i++)
        {
            rigidSwitches[i].ToOrthographic();
        }
    }
}
