using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class RigidSwitch : ProjectionBase
{
    protected Rigidbody2D rigid2D;
    public Rigidbody2D Rigid2D => rigid2D;

    protected override void Awake()
    {
        base.Awake();

        rigid2D = GetComponent<Rigidbody2D>();
    }

    [SerializeField]
    protected float gravityScale = 3f;

    [Space(5)]

    [SerializeField]
    protected float OrthDrag = 3f;
    [SerializeField]
    protected float PersDrag = 3f;

    [SerializeField]
    protected int isOnCliff = 0; // Perspective 절벽에 올라 서 있는지 여부
    public int IsOnCliff => isOnCliff;

    protected Vector2 lastVelocity = Vector2.zero;

    /*
    public void ProjectionChange(bool orthographic)
    {
        if (orthographic) // 2D가 됐을 경우
        {
            ToOrthographic();
        }
        else // 3D가 됐을 경우
        {
            ToPerspective();
        }
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cliff"))
        {
            isOnCliff++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cliff"))
        {
            isOnCliff--;

            if (isOnCliff <= 0 && !ProjectionManager.Inst.Orthographic) // 절벽에서 떨어졌는데 Perspective일 경우
            {
                GetComponent<Collider2D>().enabled = false;

                StartCoroutine("FallToCliff");
            }
        }
    }
    protected virtual void CliffFallStart()
    {

    }

    private IEnumerator FallToCliff()
    {
        CliffFallStart();

        changeable = false;
        transform.parent = null;

        float time = 0f;
        float curSpeed = 0f;

        while (time <= 2f)
        {
            transform.localPosition += Vector3.forward * curSpeed;
            time += Time.deltaTime;

            curSpeed += Time.deltaTime * 0.5f;
            yield return null;
        }

        DeleteThis();
    }
    protected virtual void DeleteThis()
    {
        StopCoroutine("FallToCliff");
        gameObject.SetActive(false);
    }

    public override void ToOrthoComplete()
    {
        rigid2D.velocity = lastVelocity;

        rigid2D.drag = OrthDrag;

        rigid2D.gravityScale = gravityScale;
    }
    public override void ToPerspStart()
    {
        lastVelocity = rigid2D.velocity;
        rigid2D.velocity = Vector2.zero;

        rigid2D.drag = PersDrag;

        rigid2D.gravityScale = 0f;
    }

    public override void ToPerspComplete()
    {
        if (isOnCliff <= 0 && !ProjectionManager.Inst.Orthographic) // 절벽에서 떨어졌는데 Perspective일 경우
        {
            StartCoroutine("FallToCliff");
        }
    }
}
