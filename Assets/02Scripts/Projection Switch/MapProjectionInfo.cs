using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapProjectionInfo : MonoBehaviour
{
    [SerializeField]
    private bool orthographic = false;

    private void Start()
    {
        if (ProjectionManager.Inst.Orthographic != orthographic) // ProjectionManager와 맵의 Orthographic이 다를 경우
        {
            ProjectionManager.Inst.ProjectionChange();
        }
    }
}
