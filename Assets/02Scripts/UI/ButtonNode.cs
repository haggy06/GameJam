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

public class ButtonNode : MonoBehaviour
{
    #region _Nodes_
    [SerializeField]
    private ButtonNode node_Left;
    public ButtonNode Node_Left => node_Left;

    [SerializeField]
    private ButtonNode node_Right;
    public ButtonNode Node_Right => node_Right;

    [SerializeField]
    private ButtonNode node_Up;
    public ButtonNode Node_Up => node_Up;

    [SerializeField]
    private ButtonNode node_Down;
    public ButtonNode Node_Down => node_Down;
    #endregion

    protected Button button;
    protected virtual void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetNode(Direction direction, ButtonNode newNode)
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

    [ContextMenu("Button Click")]
    public void ButtonClick()
    {
        button.Select();

        button.onClick.Invoke();
    }
}
