using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualInteract : InteractBase
{
    [SerializeField]
    private string manualName;

    public override void Interact()
    {
        Debug.Log("���� ��ħ");

        PopupManager.Inst.Manual_Popup.SetManualImage(manualName);

        /*
        GetComponent<Collider2D>().enabled = false;
        LeanTween.alpha(gameObject, 0f, 0.1f);
        */
    }
}
