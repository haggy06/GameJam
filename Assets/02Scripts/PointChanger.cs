using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointChanger : MonoBehaviour
{
    private Transform camTrans;
    private Camera cam;

    [SerializeField]
    private float requireTime = 1f;

    private void Awake()
    {
        camTrans = GameObject.FindWithTag("MainCamera").transform;

        cam = camTrans.GetComponent<Camera>();
    }

    [SerializeField]
    private bool isPerspective = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("시점 전환");

            if (isPerspective) // 원근감 ON
            {
                LeanTween.value(camTrans.gameObject, camTrans.position.z, -1000f, requireTime).setEase(LeanTweenType.linear).setOnUpdate((float value) => { PositionUpdate(value); });

                LeanTween.value(camTrans.gameObject, cam.fieldOfView, 1.03f, requireTime).setEase(LeanTweenType.easeOutCirc).setOnUpdate((float value) => { FOVUpdate(value); });
            }
            else // 원근감 OFF
            {
                LeanTween.value(camTrans.gameObject, camTrans.position.z, -19.8f, requireTime).setEase(LeanTweenType.linear).setOnUpdate((float value) => { PositionUpdate(value); });

                LeanTween.value(camTrans.gameObject, cam.fieldOfView, 50f, requireTime).setEase(LeanTweenType.easeInCirc).setOnUpdate((float value) => { FOVUpdate(value); });
            }

            isPerspective = !isPerspective;
        }
    }


    [SerializeField]
    private Vector3 vec = Vector3.zero;
    
    [SerializeField]
    private float fov;
    private void PositionUpdate(float value)
    {
        vec.z = value;

        camTrans.position = vec;
    }
    private void FOVUpdate(float value)
    {
        cam.fieldOfView = value;
    }
}
