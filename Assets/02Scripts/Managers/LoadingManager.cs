using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    private float waitTime = 0.5f;
    private void Awake()
    {
        PopupManager.Inst.Fade.PopupHide();

        PopupManager.Inst.NewSceneLoaded();
        ProjectionManager.Inst.ProjectionListClear();

        StartCoroutine("LoadAsyncScene");
    }

    private IEnumerator LoadAsyncScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(GameManager.Inst.CurScene.ToString());

        asyncOperation.allowSceneActivation = false;

        Debug.Log("�� �ε� �Ͻ�����");

        yield return YieldInstructionCache.WaitForSeconds(waitTime);

        Debug.Log("�� �ε� �Ͻ����� ����");

        while (!Mathf.Approximately(asyncOperation.progress, 0.9f))
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;

        GameManager.Inst.SceneLoadComplete();
    }
}
