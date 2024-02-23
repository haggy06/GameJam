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

            if (collision.transform.parent != null) // ���� �ع� ����
            {
                if (!collision.transform.parent.TryGetComponent<Rigidbody2D>(out rigid2D)) // ���� ������Ʈ�� �θ𿡰� Rigidbody2D�� �־��� ��� rigid2D�� ����
                {
                    Debug.Log(" rigid2D ���� ����(�θ�)");
                }
            }
            else
            {
                if (!collision.TryGetComponent<Rigidbody2D>(out rigid2D)) // ���� ������Ʈ�� Rigidbody2D�� �־��� ��� rigid2D�� ����
                {
                    Debug.Log(" rigid2D ���� ����");
                }
            }


            if (rigid2D != null)
            {
                if (rigid2D.velocity.y < 0) // �������� �־��� ���
                {
                    Vector2 velo;
                    velo.x = rigid2D.velocity.x;
                    velo.y = (-rigid2D.velocity.y) * bouncePower;

                    rigid2D.velocity = velo;
                    Debug.Log(collision.gameObject.name + " ƨ�ܳ�");

                    AudioManager.Inst.PlaySFX(SFX.Trampoline);
                    anim.SetTrigger(bounceHash); // �ٿ �ִϸ��̼� ����
                }
            }
        }
    }
}
