using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class VirtualButton : ButtonNode
{
    public void SetNode(ButtonNode newNode)
    {
        node_Up = node_Right = node_Left = node_Down = newNode;
    }

    protected override void Awake()
    {
        button = GetComponent<Button>();
    }

    public override void ButtonClick()    {    }
    public override void ButtonHighlight()    {    }
    public override void ButtonNormal()    {    }
    public override void Highlighted()    {    }
}
