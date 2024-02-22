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

            if (collision.transform.parent != null) // 쏘유 해버 마더
            {
                if (collision.transform.parent.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid2D)) // 닿은 오브젝트의 부모에게 Rigidbody2D가 있었을 경우
                {
                    if (rigid2D.velocity.y <= 0) // 떨어지고 있었을 경우
                    {
                        rigid2D.velocity = Vector2.right * rigid2D.velocity.x;

                        collision.transform.parent.gameObject.layer = (int)LAYER.Standable_Entity;
                    }
                }
            }
            else
            {
                if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid2D)) // 닿은 오브젝트에 Rigidbody2D가 있었을 경우
                {
                    if (rigid2D.velocity.y <= 0) // 떨어지고 있었을 경우
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
            if (collision.transform.parent != null) // 쏘유 해버 마더
            {
                if (collision.transform.parent.gameObject.layer == (int)LAYER.Standable_Entity) // 발판을 밟을 수 있는 오브젝트였을 경우
                {
                    collision.transform.parent.gameObject.layer = (int)LAYER.Unstandable_Entity;
                }
            }
            else
            {
                if (collision.gameObject.layer == (int)LAYER.Standable_Entity) // 발판을 밟을 수 있는 오브젝트였을 경우
                {
                    collision.gameObject.layer = (int)LAYER.Unstandable_Entity;
                }
            }
        }
    }
}
