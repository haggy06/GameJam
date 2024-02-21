using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public enum Direction
{
    LEFT,
    RIGHT,
    UP,
    DOWN,

}

internal class ButtonHash
{
    private static int normal = Animator.StringToHash("Normal");
    public static int Normal => normal;


    private static int highlight = Animator.StringToHash("Highlighted");
    public static int Highlight => highlight;


    private static int pressed = Animator.StringToHash("Pressed");
    public static int Pressed => pressed;


    private static int selected = Animator.StringToHash("Selected");
    public static int Selected => selected;


    private static int disabled = Animator.StringToHash("Disabled");
    public static int Disabled => disabled;
}


[RequireComponent(typeof(Button))]
public class ButtonNode : MonoBehaviour
{
    #region _Nodes_
    [SerializeField]
    protected ButtonNode node_Left;
    public ButtonNode Node_Left => node_Left;

    [SerializeField]
    protected ButtonNode node_Right;
    public ButtonNode Node_Right => node_Right;

    [SerializeField]
    protected ButtonNode node_Up;
    public ButtonNode Node_Up => node_Up;

    [SerializeField]
    protected ButtonNode node_Down;
    public ButtonNode Node_Down => node_Down;
    #endregion

    public virtual void SetNode(Direction direction, ButtonNode newNode)
    {
        switch (direction)
        {
            case Direction.LEFT:
                node_Left = newNode;
                break;

            case Direction.RIGHT:
                node_Right = newNode;
                break;

            case Direction.UP:
                node_Up = newNode;
                break;

            case Direction.DOWN:
                node_Down = newNode;
                break;
        }
    }


    protected Button button;
    public Button Button => button;

    protected Animator anim;
    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        anim = GetComponent<Animator>();

        button.onClick.AddListener(() => button.Select());
    }

    [ContextMenu("Button Click")]
    public virtual void ButtonClick()
    {
        Debug.Log(gameObject.name + " 클릭");

        anim.SetTrigger(ButtonHash.Pressed);

        Invoke("EventStart", 0.125f);
    }
    private void EventStart()
    {
        anim.SetTrigger(ButtonHash.Selected);

        button.onClick.Invoke();
    }

    public virtual void ButtonHighlight()
    {
        Debug.Log(gameObject.name + " 하이라이트");

        anim.SetTrigger(ButtonHash.Highlight);

        PopupManager.Inst.VirtualButton.Button.Select();
    }
    public virtual void ButtonNormal()
    {
        Debug.Log(gameObject.name + " 정상화");

        anim.SetTrigger(ButtonHash.Normal);
    }

    public virtual void Highlighted()
    {
        if (PopupManager.Inst.CurButton != this)
        {
            PopupManager.Inst.CurButton = this;
        }
    }
}
