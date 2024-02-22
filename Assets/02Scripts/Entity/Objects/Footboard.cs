using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footboard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FootPosition"))
        {
            Debug.Log(collision.gameObject.name);

            if (collision.transform.parent != null) // ���� �ع� ����
            {
                if (collision.transform.parent.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid2D)) // ���� ������Ʈ�� �θ𿡰� Rigidbody2D�� �־��� ���
                {
                    if (rigid2D.velocity.y <= 0) // �������� �־��� ���
                    {
                        rigid2D.velocity = Vector2.right * rigid2D.velocity.x;

                        collision.transform.parent.gameObject.layer = (int)LAYER.Standable_Entity;
                    }
                }
            }
            else
            {
                if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid2D)) // ���� ������Ʈ�� Rigidbody2D�� �־��� ���
                {
                    if (rigid2D.velocity.y <= 0) // �������� �־��� ���
                    {
                        rigid2D.velocity = Vector2.right * rigid2D.velocity.x;

                        collision.gameObject.layer = (int)LAYER.Standable_Entity;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FootPosition"))
        {
            if (collision.transform.parent != null) // ���� �ع� ����
            {
                if (collision.transform.parent.gameObject.layer == (int)LAYER.Standable_Entity) // ������ ���� �� �ִ� ������Ʈ���� ���
                {
                    collision.transform.parent.gameObject.layer = (int)LAYER.Unstandable_Entity;
                }
            }
            else
            {
                if (collision.gameObject.layer == (int)LAYER.Standable_Entity) // ������ ���� �� �ִ� ������Ʈ���� ���
                {
                    collision.gameObject.layer = (int)LAYER.Unstandable_Entity;
                }
            }
        }
    }
}
