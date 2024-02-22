using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    private void Awake()
    {
        FieldON(isFieldON);
    }
    [SerializeField]
    private bool isFieldON = false;

    [SerializeField, Tooltip("{slowedMoveSpeed, slowedJumpPower, slowedTerminalVelocity}")]
    private float[] slowedPhysics = { 3f, 6f, -5 };

    private float[] originalPhysics = new float[3];
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFieldON)
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController player)) // PlayerController�� �ִ� ������Ʈ���� ���
            {
                originalPhysics[0] = player.MoveSpeed;
                originalPhysics[1] = player.JumpPower;
                originalPhysics[2] = player.TerminalVelocity;


                player.PhysicalChange(slowedPhysics[0], slowedPhysics[1], slowedPhysics[2], true);
            }
            else if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid2D))// PlayerController�� ������ Rigidbody2D�� �ִ� ������Ʈ���� ���
            {

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isFieldON)
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController player)) // PlayerController�� �ִ� ������Ʈ���� ���
            {
                player.PhysicalChange(originalPhysics[0], originalPhysics[1], originalPhysics[2], false);
            }
            else if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid2D))// PlayerController�� ������ Rigidbody2D�� �ִ� ������Ʈ���� ���
            {

            }
        }
    }

    [Space(10)]

    [SerializeField]
    private Color fieldOnColor;
    [SerializeField]
    private Color fieldOffColor;

    public void FieldON(bool b)
    {
        isFieldON = b;

        if (b)
        {
            GetComponent<ColorSwitch>().OrthoColor = fieldOnColor;

            GetComponent<ColorSwitch>().ToOrthoStart();
        }
        else
        {
            GetComponent<ColorSwitch>().OrthoColor = fieldOffColor;

            GetComponent<ColorSwitch>().ToOrthoStart();
        }
    }
}
