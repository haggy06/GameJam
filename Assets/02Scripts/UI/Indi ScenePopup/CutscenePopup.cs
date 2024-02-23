using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CutscenePopup : PopupBase
{
    private void Start()
    {
        PopupHide();
    }
    protected override void OnActive()
    {
        base.OnActive();

        AudioManager.Inst.BGM = BGM.Cutscene;
        StartCoroutine("CutsceneInputCheck");
    }
    protected override void OnDeActive()
    {
        base.OnDeActive();

        AudioManager.Inst.BGM = BGM.Title;
        StopCoroutine("CutsceneInputCheck");
    }
    [Space(5), SerializeField]
    private int cutSceneIndex = 0;
    private IEnumerator CutsceneInputCheck()
    {
        cutSceneIndex = 0;

        AllCutsceneClose();
        CutsceneOpen(cutSceneIndex);

        while (true)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("´ÙÀ½ ÆË¾÷");

                cutSceneIndex++;

                if (cutSceneIndex < 4)
                {
                    transform.GetChild(cutSceneIndex).GetComponent<PopupBase>().PopupFadeIn(null);

                    yield return YieldInstructionCache.WaitForSeconds(closeRequireTime * 1.5f);
                }
                else
                {
                    PopupManager.Inst.PopupFadeOut();
                }
            }
        }
    }

    [SerializeField]
    private float closeRequireTime = 0.1f;
    private void AllCutsceneClose()
    {
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).GetComponent<PopupBase>().PopupHide();
        }
    }
    private void CutsceneOpen(int index)
    {
        transform.GetChild(index).GetComponent<PopupBase>().PopupFadeIn(null);
    }
}
