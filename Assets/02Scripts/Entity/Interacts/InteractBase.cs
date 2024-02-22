using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBase : MonoBehaviour
{
    [SerializeField]
    protected bool interactable = true;
    public bool Interactable
    {
        get => interactable;
        set => interactable = value;
    }

    protected virtual void Awake()
    {
        gameObject.tag = "InteractableEntity";
    }

    public virtual void Interact()
    {

    }
}
