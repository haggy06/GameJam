using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartBGM : MonoBehaviour
{
    [SerializeField]
    private BGM bgm;

    private void Awake()
    {
        AudioManager.Inst.BGM = bgm;
        Debug.Log("¼³Á¤");
    }
}
