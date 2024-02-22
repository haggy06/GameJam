using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionBase : MonoBehaviour
{

    protected bool changeable = true;
    public bool Changeable => changeable;

    protected virtual void Awake()
    {
        ProjectionManager.Inst.InputList(this);
    }

    public virtual void ToOrthoStart()
    {

    }
    public virtual void ToPerspStart()
    {

    }

    public virtual void ToOrthoComplete()
    {

    }
    public virtual void ToPerspComplete()
    {

    }
}
