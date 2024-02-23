using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public enum SFX
{
    Move,
    Jump,
    Fall,
    Portal,
    Button,
    MagneticON,
    MagneticOFF,
    Trampoline,

}

public enum BGM
{
    Mute,
    Title,
    Cutscene,
    Painting,
    Cyberpunk,

}

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField]
    private AudioSource bgmSpeacker;
    [SerializeField]
    private BGM bgm;
    public BGM BGM
    {
        get => bgm;
        set
        {
            if (bgm != value)
            {
                bgm = value;

                ChangeBGM(bgm);
            }
        }
    }
    [SerializeField]
    private Transform speackerBox;
    [SerializeField]
    private List<AudioSource> fbxSpeakers = new List<AudioSource>();

    private void ChangeBGM(BGM bgm)
    {
        AudioClip clip = Resources.Load<AudioClip>(Path.Combine("Sound", (Path.Combine("BGM", bgm.ToString()))));

        bgmSpeacker.Pause();

        bgmSpeacker.clip = clip;
        bgmSpeacker.Play();

        LeanTween.value(0.5f, 1f, 0.5f).setOnUpdate((float value) => bgmSpeacker.volume = value);
    }

    public void PlaySFX(SFX sfx)
    {
        AudioClip clip = Resources.Load<AudioClip>(Path.Combine("Sound" , (Path.Combine("SFX", sfx.ToString()))));

        bool isEmpty = false;
        int i;
        for (i = 0; i < fbxSpeakers.Count; i++)
        {
            if (!fbxSpeakers[i].isPlaying) // 사운드 재생 중이 아닐 경우
            {
                isEmpty = true;

                break;
            }
        }

        if (isEmpty) // 빈 스피커가 있었다면
        {
            fbxSpeakers[i].clip = clip;
            fbxSpeakers[i].Play();
        }
        else // 빈 스피커가 없었다면
        {
            AudioSource newSpeacker = CreateSpeacker();
            
            newSpeacker.clip = clip;
            newSpeacker.Play();

            fbxSpeakers.Add(newSpeacker);
        }
    }

    private AudioSource CreateSpeacker()
    {
        GameObject obj = new GameObject("Speaker " + fbxSpeakers.Count);
        obj.transform.parent = speackerBox;

        AudioSource speacker = obj.AddComponent<AudioSource>();

        speacker.playOnAwake = false;
        speacker.loop = false;

        return speacker;
    }
}
