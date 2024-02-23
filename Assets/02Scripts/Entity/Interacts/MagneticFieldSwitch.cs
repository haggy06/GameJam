using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticFieldSwitch : InteractBase
{
    [SerializeField]
    private MagneticField magneticField;
    public MagneticField Magnetic_Field
    {
        get => magneticField;
        set => magneticField = value;
    }
    [SerializeField]
    private bool isOnSwitch = true;

    [SerializeField]
    private bool oneUse = true;

    public override void Interact()
    {
        base.Interact();

        Debug.Log("자기장 상호작용");

        magneticField.FieldON(isOnSwitch);

        if (oneUse)
        {
            interactable = false;

            Destroy(GetComponent<ColorSwitch>());

            LeanTween.alpha(gameObject, 0f, 0.25f).setEase(LeanTweenType.easeOutCirc);
        }
        else
        {
            isOnSwitch = !isOnSwitch;
        }
    }
}
