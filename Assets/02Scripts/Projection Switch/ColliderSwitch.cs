using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSwitch : ProjectionBase
{
    [SerializeField]
    private Collider2D perspCollider;
    [SerializeField]
    private Collider2D orthoCollider;


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
