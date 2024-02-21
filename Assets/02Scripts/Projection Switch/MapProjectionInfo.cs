using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapProjectionInfo : MonoBehaviour
{
    [SerializeField]
    private bool orthographic = false;

    private void Start()
    {
        if (ProjectionManager.Inst.Orthographic != orthographic) // ProjectionManager�� ���� Orthographic�� �ٸ� ���
        {
            ProjectionManager.Inst.ProjectionChange();
        }
    }
}
