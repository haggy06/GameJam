using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformChangeInteract : InteractBase
{
    [SerializeField]
    private float duration;
    [SerializeField]
    private LeanTweenType leanType;

    [Space(5)]

    [SerializeField]
    private GameObject target;

    [SerializeField, Tooltip("{position, rotation, size}")]
    private Vector3[] targetTransform = new Vector3[3];

    [Header("Change Decide")]

    [SerializeField]
    private bool positionChange = true;
    [SerializeField]
    private bool rotationChange = true;
    [SerializeField]
    private bool sizeChange = true;

    [SerializeField]
    private Animator anim;
    public override void Interact()
    {
        base.Interact();

        if (positionChange)
            LeanTween.moveLocal(target, targetTransform[0], duration).setEase(leanType);

        if (rotationChange)
            LeanTween.rotateLocal(target, targetTransform[1], duration).setEase(leanType);

        if (sizeChange)
            LeanTween.scale(target, targetTransform[2], duration).setEase(leanType);


        interactable = false;

        //Destroy(GetComponent<ColorSwitch>());

        LeanTween.alpha(gameObject, 0f, 0.25f).setEase(LeanTweenType.easeOutCirc);

        if (anim != null)
        {
            anim.SetTrigger("ON");
        }
    }
}
