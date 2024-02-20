using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class RigidSwitch : MonoBehaviour
{
    protected Rigidbody2D rigid2D;
    public Rigidbody2D Rigid2D => rigid2D;

    protected virtual void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();

        GameManager.Inst.InputList(this);
    }

    [SerializeField]
    protected float gravityScale = 3f;

    [Space(5)]

    [SerializeField]
    protected float OrthDrag = 3f;
    [SerializeField]
    protected float PersDrag = 3f;

    protected Vector2 lastVelocity = Vector2.zero;

    /*
    public void ProjectionChange(bool orthographic)
    {
        if (orthographic) // 2D°¡ µÆÀ» °æ¿ì
        {
            ToOrthographic();
        }
        else // 3D°¡ µÆÀ» °æ¿ì
        {
            ToPerspective();
        }
    }
    */

    public virtual void ToOrthographic()
    {
        rigid2D.velocity = lastVelocity;

        rigid2D.drag = OrthDrag;

        rigid2D.gravityScale = gravityScale;
    }
    public virtual void ToPerspective()
    {
        lastVelocity = rigid2D.velocity;
        rigid2D.velocity = Vector2.zero;

        rigid2D.drag = PersDrag;

        rigid2D.gravityScale = 0f;
    }
}
