using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : RigidSwitch
{
    [Space(5)]

    [SerializeField]
    private bool controllable = true;
    public bool Controllable
    {
        get => controllable;
        set => controllable = value;
    }

    [Space(5)]

    [SerializeField]
    private float moveSpeed = 5f;
    public float MoveSpeed => moveSpeed;
    
    
    [SerializeField]
    private float jumpPower = 10f;
    public float JumpPower => jumpPower;


    [SerializeField]
    private float terminalVelocity = -20f;
    public float TerminalVelocity => terminalVelocity;


    private Vector2 moveDir;

    private bool isGround = false;

    private Collider2D col2D;
    protected override void Awake()
    {
        base.Awake();

        TryGetComponent<Collider2D>(out col2D);
    }

    private Vector2 footPosition;
    private Vector2 terminalVelo;
    private void FixedUpdate()
    {
        footPosition.x = col2D.bounds.center.x;
        footPosition.y = col2D.bounds.min.y;

        isGround = Physics2D.OverlapCircle(footPosition, 0.1f, 1 << (int)LAYER.Ground | 1 << (int)LAYER.Footboard);

        if (rigid2D.velocity.y < terminalVelocity)
        {
            terminalVelo.x = rigid2D.velocity.x;
            terminalVelo.y = terminalVelocity;

            rigid2D.velocity = terminalVelo;
        }
    }

    public void PhysicalChange(float newMoveSpeed, float newJumpPower, float newTerminalVelocity, bool changeOrBack)
    {
        moveSpeed = newMoveSpeed;
        jumpPower = newJumpPower;
        terminalVelocity = newTerminalVelocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(footPosition, 0.1f);
    }

    private Vector2 tempVec;

    private float rotateValue;
    private void Update()
    {
        if (controllable)
        {
            #region _Interact Logic_
            if (curInteract != null && Input.GetKeyDown(KeyCode.A))
            {
                curInteract.Interact();
            }
            #endregion

            #region _Move Logic_
            if (ProjectionManager.Inst.Orthographic) // 플랫포머 상태
            {
                #region _Move Logic_
                moveDir.x = Input.GetAxisRaw("Horizontal");

                if (moveDir.x != 0f)
                {
                    rotateValue = moveDir.x > 0f ? 0f : 180f;

                    transform.GetChild(0).localEulerAngles = Vector3.up * rotateValue;
                }

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
            else // 탑뷰 상태
            {
                moveDir.x = Input.GetAxisRaw("Horizontal");
                moveDir.y = Input.GetAxisRaw("Vertical");

                if (moveDir != Vector2.zero)
                {
                    rotateValue = -(Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg);

                    transform.GetChild(0).localEulerAngles = Vector3.up * rotateValue;
                }

                rigid2D.velocity = moveDir * moveSpeed;
            }
            #endregion

            #region _Projection Change Logic_
            if (Input.GetKeyDown(KeyCode.LeftShift)) // 시점 전환
            {
                ProjectionManager.Inst.ProjectionChange();
            }
            #endregion
        }
    }

    public override void ToOrthoStart()
    {
        if (90f < transform.GetChild(0).localEulerAngles.y && transform.GetChild(0).localEulerAngles.y < 270f) // 왼쪽을 보고 있었을 경우
        {
            LeanTween.rotate(transform.GetChild(0).gameObject,  Vector3.up * 180f, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
        }
        else // 오른쪽을 보고 있었을 경우
        {
            LeanTween.rotate(transform.GetChild(0).gameObject, Vector3.up * 0f, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
        }

        LeanTween.rotateX(gameObject, -0f, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);

        rigid2D.velocity = Vector2.zero;

        controllable = false;

        /*
        transform.GetChild(1).gameObject.SetActive(true);

        
        LeanTween.value(0f, 3f, ProjectionManager.Inst.Duration).setOnUpdate((float value) => LightUpdate(value));
        LeanTween.rotateX(transform.GetChild(1).gameObject, -0f, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
        */
    }
    private void LightUpdate(float value)
    {
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            transform.GetChild(1).GetChild(i).GetComponent<Light>().range = value;
        }
    }
    protected override void FallStart()
    {
        controllable = false;

        Debug.Log("게임 오버");
    }

    public override void ToOrthoComplete()
    {
        rigid2D.drag = OrthDrag;

        rigid2D.gravityScale = gravityScale;

        controllable = true;
    }

    public override void ToPerspStart()
    {
        LeanTween.rotateX(gameObject, -60f, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);

        base.ToPerspStart();

        controllable = false;

        /*
        //transform.GetChild(1).gameObject.SetActive(false);
        
        LeanTween.value(3f, 0f, ProjectionManager.Inst.Duration).setOnUpdate((float value) => LightUpdate(value));
        LeanTween.rotateX(transform.GetChild(1).gameObject, 60f, ProjectionManager.Inst.Duration).setEase(ProjectionManager.Inst.LeanType);
        */
        
    }
    public override void ToPerspComplete()
    {
        controllable = true;

        base.ToPerspComplete();
    }


    private InteractBase curInteract;
    protected override void OnTriggerEnter2D_Override(Collider2D collision)
    {
        if (collision.CompareTag("InteractableEntity"))
        {
            if ((collision.TryGetComponent<InteractBase>(out InteractBase interact)))
            {
                curInteract = interact;
            }
        }
    }
    protected override void OnTriggerExit2D_Override(Collider2D collision)
    {
        if (collision.CompareTag("InteractableEntity"))
        {
            if (collision.TryGetComponent<InteractBase>(out InteractBase interact))
            {
                curInteract = null;
            }
        }
    }
}
