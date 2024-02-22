using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InteractPopup : PopupBase
{
    [SerializeField]
    private Image fillImage;

    public void InteractStart(float requireTime)
    {
        this.requireTime = requireTime;

        PopupManager.Inst.PopupFadeIn(this, null);
        StartCoroutine("GaugeFill");
    }
    public void InteractStop()
    {
        StopCoroutine("GaugeFill");

        fillImage.fillAmount = 0f;
    }

    protected override void OnActive()
    {
        base.OnActive();

        fillImage.fillAmount = 0f;
        StopCoroutine("FollowTarget");
        StartCoroutine("FollowTarget");
    }
    private IEnumerator FollowTarget()
    {
        while (true)
        {
            transform.position = Camera.main.WorldToScreenPoint(GameManager.Inst.CurPlayer.CurInteract.gameObject.transform.position + (Vector3.up * 2));

            yield return null;
        }
    }

    private float requireTime;
    private IEnumerator GaugeFill()
    {
        fillImage.fillAmount = 0f;

        float percent = 0f;
        while(percent < 1f)
        {
            percent += Time.deltaTime / requireTime;

            fillImage.fillAmount = percent;

            yield return null;
        }

        Debug.Log("��ȣ�ۿ� ����");

        GameManager.Inst.CurPlayer.CurInteract.Interact();
        PopupManager.Inst.PopupFadeOut();
    }

    protected override void OnDeActive()
    {
        base.OnDeActive();

        StopCoroutine("GaugeFill");
        StopCoroutine("FollowTarget");
    }
}
