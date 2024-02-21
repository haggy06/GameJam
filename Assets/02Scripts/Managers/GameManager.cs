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
        if (Time.timeScale == 0f) // 일시정지가 되어 있었을 경우
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

        Debug.Log("새로운 씬 실행 : " + SceneManager.GetActiveScene().buildIndex);

        PopupManager.Inst.NewSceneLoaded();

        if (SceneManager.GetActiveScene().buildIndex > 3) // 현재 씬이 Intro(0), Loading(1), Title(2), StageSelect(3) 씬이 아닐 경우
        {
            Debug.Log("게임 씬 로드됨");

            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                playerObj.TryGetComponent<PlayerController>(out player);
            }
            else
            {
                Debug.LogError("현재 씬에 플레이어가 없습니다");
            }
        }
    }


    public void THEWORLD(bool isOn)
    {
        if (isOn) // 일시정지 ON
        {
            Time.timeScale = 0f;

            if (CurPlayer != null)
            {
                CurPlayer.Controllable = false;
            }
        }
        else // 일시정지 OFF
        {
            Time.timeScale = 1f;

            if (CurPlayer != null)
            {
                CurPlayer.Controllable = true;
            }
        }
    }
}