using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public enum SCENE
{
    Intro,
    Loading,
    Title,
    StageSelect,

}

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private PlayerController player;
    public PlayerController CurPlayer => player;

    [SerializeField]
    private SCENE curScene;
    public SCENE CurScene => curScene;

    public void SceneMove(SCENE targetScene)
    {
        curScene = targetScene;

        PopupManager.Inst.Fade.Fade(FadeType.SceneMove);
    }
    public void GoLoadingScene()
    {
        SceneManager.LoadScene((int)SCENE.Loading);
    }
    public void GoNextScene()
    {

    }



    public void SceneLoadComplete()
    {
        StartCoroutine("NextSceneLoaded");
    }
    private IEnumerator NextSceneLoaded()
    {
        yield return null;

        Debug.Log("���ο� �� ���� : " + SceneManager.GetActiveScene().buildIndex);

        PopupManager.Inst.NextSceneLoaded();

        if (SceneManager.GetActiveScene().buildIndex > 3) // ���� ���� Intro(0), Loading(1), Title(2), StageSelect(3) ���� �ƴ� ���
        {
            Debug.Log("���� �� �ε��");

            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                playerObj.TryGetComponent<PlayerController>(out player);
            }
            else
            {
                Debug.LogError("���� ���� �÷��̾ �����ϴ�");
            }
        }
    }
}