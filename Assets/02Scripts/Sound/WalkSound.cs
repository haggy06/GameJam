using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    public void Step()
    {
        AudioManager.Inst.PlaySFX(SFX.Move);
    }
}
