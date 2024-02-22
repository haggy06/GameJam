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

        StartCoroutine("CutsceneInputCheck");
    }
    protected override void OnDeActive()
    {
        base.OnDeActive();

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
                    AllCutsceneClose();
                    CutsceneOpen(cutSceneIndex);

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
            if (transform.GetChild(i).GetComponent<Image>().color.a > 0) // ÄÆ¾ÀÀÌ ¿­·ÁÀÖÀ» °æ¿ì
            {
                //LeanTween.alpha(transform.GetChild(i).gameObject, 0f, closeRequireTime);
                //LeanTween.color(transform.GetChild(index).gameObject, Color.white, closeRequireTime).setOnUpdate((Color value) => transform.GetChild(index).GetComponent<Image>().color = value);

                transform.GetChild(i).GetComponent<Image>().color = Color.clear;
            }
        }
    }
    private void CutsceneOpen(int index)
    {
        //LeanTween.alpha(transform.GetChild(index).gameObject, 0f, closeRequireTime);
        //LeanTween.color(transform.GetChild(i).gameObject, Color.clear, closeRequireTime).setOnUpdate((Color value) => transform.GetChild(i).GetComponent<Image>().color = value);

        transform.GetChild(index).GetComponent<Image>().color = Color.white;
    }
}
