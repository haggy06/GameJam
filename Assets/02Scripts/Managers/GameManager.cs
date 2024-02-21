using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

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
        if (Time.timeScale == 0f) // �Ͻ������� �Ǿ� �־��� ���
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene((int)SCENE.Loading);
    }
    public void GoNextScene()
    {

    }



    public void SceneLoadComplete()
    {
        StartCoroutine("NewSceneLoaded");
    }
    private IEnumerator NewSceneLoaded()
    {
        yield return null;

        Debug.Log("���ο� �� ���� : " + SceneManager.GetActiveScene().buildIndex);

        PopupManager.Inst.NewSceneLoaded();

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


    public void THEWORLD(bool isOn)
    {
        if (isOn) // �Ͻ����� ON
        {
            Time.timeScale = 0f;

            if (CurPlayer != null)
            {
                CurPlayer.Controllable = false;
            }
        }
        else // �Ͻ����� OFF
        {
            Time.timeScale = 1f;

            if (CurPlayer != null)
            {
                CurPlayer.Controllable = true;
            }
        }
    }
}