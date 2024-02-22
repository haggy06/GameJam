using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapProjectionInfo : MonoBehaviour
{
    [SerializeField]
    private bool orthographic = false;

    private void Start()
    {
        ProjectionManager.Inst.SetProjection(orthographic);

        Debug.Log("초기설정) Orthographic = " + orthographic);
    }
}
