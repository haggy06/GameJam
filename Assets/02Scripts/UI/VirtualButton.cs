using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class VirtualButton : ButtonNode
{
    public void SetNode(ButtonNode newNode)
    {
        node_Up = newNode;
        node_Right = newNode;
        node_Left = newNode;
        node_Down = newNode;

        if (newNode != null)
        {
            Debug.Log(newNode.gameObject.name + "으로 노드 설정 완료");
        }
        else
        {
            Debug.Log("null로 노드 초기화 완료");
        }
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
