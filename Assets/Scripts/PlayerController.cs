using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : RigidSwitch
{
    [Space(5)]

    [SerializeField]
    private bool controllable = true;

    [Space(5)]

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpPower = 10f;

    private Vector2 moveDir;

    private bool isGround = false;

    private Collider2D col2D;
    protected override void Awake()
    {
        base.Awake();

        TryGetComponent<Collider2D>(out col2D);
    }

    private Vector2 footPosition;
    private void FixedUpdate()
    {
        footPosition.x = col2D.bounds.center.x;
        footPosition.y = col2D.bounds.min.y;

        isGround = Physics2D.OverlapCircle(footPosition, 0.1f, 1 << 3);
    }

    private Vector2 tempVec;
    private void Update()
    {
        if (controllable)
        {
            if (GameManager.Inst.Orthographic)
            {
                #region _Move Logic_
                moveDir.x = Input.GetAxisRaw("Horizontal");

                tempVec.x = moveDir.x * moveSpeed;
                tempVec.y = rigid2D.velocity.y;

                rigid2D.velocity = tempVec;
                #endregion

                if (isGround && Input.GetKeyDown(KeyCode.Space))
                {
                    tempVec.x = rigid2D.velocity.x;
                    tempVec.y = jumpPower;

                    rigid2D.velocity = tempVec;
                }
            }
            else
            {
                moveDir.x = Input.GetAxisRaw("Horizontal");
                moveDir.y = Input.GetAxisRaw("Vertical");

                rigid2D.velocity = moveDir * moveSpeed;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)) // 시점 전환
            {
                StartCoroutine("ProjectionChange");
            }
        }
    }

    public override void ToOrthographic()
    {
        rigid2D.drag = OrthDrag;

        rigid2D.gravityScale = gravityScale;
    }

    private IEnumerator ProjectionChange()
    {
        GameManager.Inst.ProjectionChange();
        rigid2D.velocity = Vector2.zero;

        controllable = false;

        yield return YieldInstructionCache.WaitForSeconds(GameManager.Inst.Duration);

        controllable = true;
    }
}
