using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField]
    private float bouncePower = 1.25f;

    [Space(5), SerializeField]
    private Animator anim;
    private int bounceHash = Animator.StringToHash("Bounce");

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FootPosition"))
        {
            Rigidbody2D rigid2D;

            if (collision.transform.parent != null) // 쏘유 해버 마더
            {
                if (!collision.transform.parent.TryGetComponent<Rigidbody2D>(out rigid2D)) // 닿은 오브젝트의 부모에게 Rigidbody2D가 있었을 경우 rigid2D로 참조
                {
                    Debug.Log(" rigid2D 참조 실패(부모)");
                }
            }
            else
            {
                if (!collision.TryGetComponent<Rigidbody2D>(out rigid2D)) // 닿은 오브젝트에 Rigidbody2D가 있었을 경우 rigid2D로 참조
                {
                    Debug.Log(" rigid2D 참조 실패");
                }
            }


            if (rigid2D != null)
            {
                if (rigid2D.velocity.y < 0) // 떨어지고 있었을 경우
                {
                    Vector2 velo;
                    velo.x = rigid2D.velocity.x;
                    velo.y = (-rigid2D.velocity.y) * bouncePower;

                    rigid2D.velocity = velo;
                    Debug.Log(collision.gameObject.name + " 튕겨냄");

                    AudioManager.Inst.PlaySFX(SFX.Trampoline);
                    anim.SetTrigger(bounceHash); // 바운스 애니메이션 실행
                }
            }
        }
    }
}
