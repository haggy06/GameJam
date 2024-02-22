using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSwitch : ProjectionBase
{
    [SerializeField]
    private Collider2D perspCollider;
    public Collider2D PerspCollider
    {
        get => perspCollider;
        set => perspCollider = value;
    }

    [SerializeField]
    private Collider2D orthoCollider;
    public Collider2D OrthoCollider
    {
        get => orthoCollider;
        set => orthoCollider = value;
    }


    public override void ToOrthoStart()
    {
        if (perspCollider != null)
        {
            perspCollider.enabled = false;
        }
        
        if (orthoCollider != null)
        {
            orthoCollider.enabled = true;
        }
    }
    public override void ToPerspStart()
    {
        if (perspCollider != null)
        {
            perspCollider.enabled = true;
        }

        if (orthoCollider != null)
        {
            orthoCollider.enabled = false;
        }
    }
}
