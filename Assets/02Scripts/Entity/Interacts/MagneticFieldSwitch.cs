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

    public override void Interact()
    {
        base.Interact();

        magneticField.FieldON(true);
    }
}
