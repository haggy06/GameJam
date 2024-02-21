using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterInfo : MonoBehaviour
{
    [SerializeField]
    private string chapterName;
    public string ChapterName => chapterName;

    [SerializeField]
    private PopupBase connectedChapter;
    public void ChapterSelect()
    {
        if (connectedChapter != null)
        {
            connectedChapter.PopupFadeIn(transform.parent.parent.parent.GetComponent<PopupBase>());
        }
        else
        {
            Debug.Log(gameObject.name + "�� ����� é�Ͱ� �����ϴ�");
        }
    }
}
